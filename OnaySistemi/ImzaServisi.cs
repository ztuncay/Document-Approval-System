using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace OnaySistemi
{
    public static class ImzaServisi
    {
        private const double ImzaGenislikPt = 80;
        private const double Imza1_XOrani = 0.40;
        private const double Imza1_YOrani = 0.92;
        private const double Imza2_XOrani = 0.75;
        private const double Imza2_YOrani = 0.92;

        private static readonly Encoding Latin1 = Encoding.GetEncoding(28591);

        public static string ImzaDosyaYoluGetir(string kullaniciAdi)
        {
            string imzaKlasoru = ConfigurationManager.AppSettings["ImzaKlasoru"];
            if (string.IsNullOrWhiteSpace(imzaKlasoru))
                throw new InvalidOperationException("App.config icinde 'ImzaKlasoru' ayari bulunamadi.");
            return Path.Combine(imzaKlasoru, kullaniciAdi + ".png");
        }

        public static string BelgeyeImzaEkle(string kaynakPdfYolu, string hedefPdfYolu,
                                              string imzaResimYolu, string imzaciAdSoyad,
                                              int imzaPozisyonu)
        {
            if (!File.Exists(kaynakPdfYolu))
                throw new FileNotFoundException("Kaynak PDF bulunamadi.", kaynakPdfYolu);
            if (!File.Exists(imzaResimYolu))
                throw new FileNotFoundException("Imza resmi bulunamadi.", imzaResimYolu);

            string hedefKlasor = Path.GetDirectoryName(hedefPdfYolu);
            if (!string.IsNullOrEmpty(hedefKlasor) && !Directory.Exists(hedefKlasor))
                Directory.CreateDirectory(hedefKlasor);

            byte[] pdfBytes = File.ReadAllBytes(kaynakPdfYolu);
            string pdfText = Latin1.GetString(pdfBytes);

            byte[] imgCompressed;
            byte[] maskCompressed;
            int imgW, imgH;
            GorselHazirla(imzaResimYolu, out imgCompressed, out maskCompressed, out imgW, out imgH);

            int prevXrefOffset = StartXrefBul(pdfText);
            int toplamNesne = ToplamNesneBul(pdfText);
            string rootRef = RootRefBul(pdfText);

            List<SayfaBilgi> sayfalar = SayfalariParsele(pdfText, pdfBytes);

            if (sayfalar.Count == 0)
                throw new InvalidOperationException("PDF'te sayfa bulunamadi.");

            double imzaYukPt = imgH * (ImzaGenislikPt / (double)imgW);
            double xO = imzaPozisyonu == 2 ? Imza2_XOrani : Imza1_XOrani;
            double yO = imzaPozisyonu == 2 ? Imza2_YOrani : Imza1_YOrani;

            int sonrakiObj = toplamNesne;
            int maskObjNo = sonrakiObj++;
            int imgObjNo = sonrakiObj++;

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(pdfBytes, 0, pdfBytes.Length);
                Dictionary<int, long> yeniXref = new Dictionary<int, long>();

                yeniXref[maskObjNo] = ms.Position;
                YazMaskXObject(ms, maskObjNo, maskCompressed, imgW, imgH);

                yeniXref[imgObjNo] = ms.Position;
                YazImageXObject(ms, imgObjNo, imgCompressed, imgW, imgH, maskObjNo);

                foreach (var sayfa in sayfalar)
                {
                    double x = sayfa.Genislik * xO - ImzaGenislikPt / 2.0;
                    double y = sayfa.Yukseklik * (1 - yO) - imzaYukPt / 2.0;

                    string cmd = string.Format(CultureInfo.InvariantCulture,
                        "q {0:F2} 0 0 {1:F2} {2:F2} {3:F2} cm /SigImg Do Q",
                        ImzaGenislikPt, imzaYukPt, x, y);

                    int overlayNo = sonrakiObj++;
                    yeniXref[overlayNo] = ms.Position;
                    YazContentStream(ms, overlayNo, cmd);

                    yeniXref[sayfa.NesneNo] = ms.Position;
                    YazGuncelSayfa(ms, sayfa, overlayNo, imgObjNo);
                }

                long yeniXrefOffset = ms.Position;
                YazXref(ms, yeniXref);
                YazTrailer(ms, sonrakiObj, rootRef, prevXrefOffset);
                byte[] sx = Latin1.GetBytes(string.Format("\nstartxref\n{0}\n%%EOF\n", yeniXrefOffset));
                ms.Write(sx, 0, sx.Length);

                File.WriteAllBytes(hedefPdfYolu, ms.ToArray());
            }
            return hedefPdfYolu;
        }

        private static void GorselHazirla(string pngYolu, out byte[] rgbCompressed, out byte[] alphaCompressed, out int w, out int h)
        {
            using (Bitmap bmp = new Bitmap(pngYolu))
            {
                w = bmp.Width;
                h = bmp.Height;
                byte[] rgb = new byte[w * h * 3];
                byte[] alpha = new byte[w * h];
                int rgbIdx = 0;
                int aIdx = 0;
                for (int py = 0; py < h; py++)
                    for (int px = 0; px < w; px++)
                    {
                        Color c = bmp.GetPixel(px, py);
                        rgb[rgbIdx++] = c.R;
                        rgb[rgbIdx++] = c.G;
                        rgb[rgbIdx++] = c.B;
                        alpha[aIdx++] = c.A;
                    }

                rgbCompressed = ZlibSikistir(rgb);
                alphaCompressed = ZlibSikistir(alpha);
            }
        }

        private static byte[] ZlibSikistir(byte[] raw)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.WriteByte(0x78); ms.WriteByte(0x9C);
                using (DeflateStream ds = new DeflateStream(ms, CompressionLevel.Optimal, true))
                    ds.Write(raw, 0, raw.Length);
                uint adler = Adler32(raw);
                ms.WriteByte((byte)(adler >> 24)); ms.WriteByte((byte)(adler >> 16));
                ms.WriteByte((byte)(adler >> 8)); ms.WriteByte((byte)(adler));
                return ms.ToArray();
            }
        }

        private static uint Adler32(byte[] d)
        {
            uint a = 1, b = 0;
            for (int i = 0; i < d.Length; i++) { a = (a + d[i]) % 65521; b = (b + a) % 65521; }
            return (b << 16) | a;
        }

        private static int StartXrefBul(string pdf)
        {
            int eof = pdf.LastIndexOf("%%EOF");
            if (eof < 0) return 0;
            int sx = pdf.LastIndexOf("startxref", eof);
            if (sx < 0) return 0;
            string p = pdf.Substring(sx + 9, eof - sx - 9).Trim();
            string[] s = p.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(s[0].Trim(), out int offset);
            return offset;
        }

        private static int ToplamNesneBul(string pdf)
        {
            Match m = Regex.Match(pdf, @"/Size\s+(\d+)", RegexOptions.RightToLeft);
            if (m.Success) return int.Parse(m.Groups[1].Value);
            int max = 0;
            foreach (Match om in Regex.Matches(pdf, @"(\d+)\s+0\s+obj"))
            {
                int n = int.Parse(om.Groups[1].Value);
                if (n > max) max = n;
            }
            return max + 1;
        }

        private static string RootRefBul(string pdf)
        {
            Match m = Regex.Match(pdf, @"/Root\s+(\d+\s+\d+\s+R)", RegexOptions.RightToLeft);
            return m.Success ? m.Groups[1].Value : "1 0 R";
        }

        private static List<SayfaBilgi> SayfalariParsele(string pdf, byte[] pdfBytes)
        {
            List<SayfaBilgi> sayfalar = DogruNesneAra(pdf);
            if (sayfalar.Count > 0) return sayfalar;

            Dictionary<int, string> objStmNesneleri = ObjStmParsele(pdf, pdfBytes);
            sayfalar = ObjStmdenSayfaBul(objStmNesneleri, pdf);
            if (sayfalar.Count > 0) return sayfalar;

            sayfalar = PagesKidsFallback(pdf, objStmNesneleri);
            return sayfalar;
        }

        private static List<SayfaBilgi> DogruNesneAra(string pdf)
        {
            List<SayfaBilgi> list = new List<SayfaBilgi>();

            foreach (Match nm in Regex.Matches(pdf, @"(\d+)\s+0\s+obj\b"))
            {
                int objNo = int.Parse(nm.Groups[1].Value);
                int basla = nm.Index + nm.Length;
                int endObj = EndobjBul(pdf, basla);
                if (endObj < 0) continue;

                string icerik = pdf.Substring(basla, endObj - basla);

                if (!SayfaMi(icerik)) continue;

                SayfaBilgi s = SayfaParseEt(objNo, icerik, pdf);
                if (s != null) list.Add(s);
            }
            return list;
        }

        private static Dictionary<int, string> ObjStmParsele(string pdf, byte[] pdfBytes)
        {
            Dictionary<int, string> sonuc = new Dictionary<int, string>();

            int searchPos = 0;
            while (true)
            {
                int typePos = pdf.IndexOf("/ObjStm", searchPos);
                if (typePos < 0) break;
                searchPos = typePos + 7;

                int lookStart = Math.Max(0, typePos - 500);
                string before = pdf.Substring(lookStart, typePos - lookStart);
                Match objMatch = Regex.Match(before, @"(\d+)\s+0\s+obj");
                if (!objMatch.Success) continue;

                int objPos = lookStart + objMatch.Index + objMatch.Length;

                int streamKw = pdf.IndexOf("stream", typePos);
                if (streamKw < 0 || streamKw - typePos > 500) continue;

                if (streamKw >= 3 && pdf.Substring(streamKw - 3, 3) == "end") continue;

                string dictText = pdf.Substring(objPos, streamKw - objPos);

                Match nM = Regex.Match(dictText, @"/N\s+(\d+)");
                Match fM = Regex.Match(dictText, @"/First\s+(\d+)");
                Match lMIndirect = Regex.Match(dictText, @"/Length\s+(\d+)\s+(\d+)\s+R");
                Match lMDirect = Regex.Match(dictText, @"/Length\s+(\d+)");
                if (!nM.Success || !fM.Success || (!lMIndirect.Success && !lMDirect.Success)) continue;

                int n = int.Parse(nM.Groups[1].Value);
                int first = int.Parse(fM.Groups[1].Value);
                int length = 0;
                if (lMIndirect.Success)
                {
                    int lenObjNo = int.Parse(lMIndirect.Groups[1].Value);
                    length = ObjLengthCozumle(pdf, pdfBytes, lenObjNo);
                }
                else if (lMDirect.Success)
                {
                    length = int.Parse(lMDirect.Groups[1].Value);
                }

                int dataStart = streamKw + 6;
                if (dataStart < pdfBytes.Length && pdfBytes[dataStart] == 0x0D) dataStart++;
                if (dataStart < pdfBytes.Length && pdfBytes[dataStart] == 0x0A) dataStart++;

                if (length <= 0)
                {
                    length = EndStreamUzunluguBul(pdfBytes, dataStart);
                }

                if (length <= 0 || dataStart + length > pdfBytes.Length) continue;

                byte[] streamData = new byte[length];
                Array.Copy(pdfBytes, dataStart, streamData, 0, length);

                byte[] decompressed = null;
                if (dictText.Contains("/FlateDecode"))
                {
                    try { decompressed = FlateDecompress(streamData); }
                    catch { continue; }
                }
                else
                {
                    decompressed = streamData;
                }

                if (decompressed == null || decompressed.Length < first) continue;

                string text = Latin1.GetString(decompressed);
                string header = text.Substring(0, Math.Min(first, text.Length));
                string body = first < text.Length ? text.Substring(first) : "";

                string[] parts = header.Trim().Split(new[] { ' ', '\n', '\r', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);

                List<int> objNos = new List<int>();
                List<int> offsets = new List<int>();
                for (int i = 0; i + 1 < parts.Length; i += 2)
                {
                    if (int.TryParse(parts[i], out int oNo) && int.TryParse(parts[i + 1], out int oOff))
                    {
                        objNos.Add(oNo);
                        offsets.Add(oOff);
                    }
                }

                for (int i = 0; i < objNos.Count; i++)
                {
                    int start = offsets[i];
                    int end = (i + 1 < offsets.Count) ? offsets[i + 1] : body.Length;
                    if (start >= body.Length) continue;
                    if (end > body.Length) end = body.Length;
                    sonuc[objNos[i]] = body.Substring(start, end - start).Trim();
                }
            }

            return sonuc;
        }

        private static byte[] FlateDecompress(byte[] data)
        {
            int skip = (data.Length >= 2 && data[0] == 0x78) ? 2 : 0;
            using (MemoryStream input = new MemoryStream(data, skip, data.Length - skip))
            using (DeflateStream ds = new DeflateStream(input, CompressionMode.Decompress))
            using (MemoryStream output = new MemoryStream())
            {
                ds.CopyTo(output);
                return output.ToArray();
            }
        }

        private static int ObjLengthCozumle(string pdf, byte[] pdfBytes, int objNo)
        {
            Match om = Regex.Match(pdf, objNo + @"\s+0\s+obj\b");
            if (!om.Success)
                return 0;

            int basla = om.Index + om.Length;
            int endObj = EndobjBul(pdf, basla);
            if (endObj < 0)
                return 0;

            string icerik = pdf.Substring(basla, endObj - basla).Trim();
            Match num = Regex.Match(icerik, @"\b(\d+)\b");
            if (num.Success && int.TryParse(num.Groups[1].Value, out int len))
                return len;

            return 0;
        }

        private static int EndStreamUzunluguBul(byte[] pdfBytes, int dataStart)
        {
            byte[] pattern = Encoding.ASCII.GetBytes("endstream");
            for (int i = dataStart; i <= pdfBytes.Length - pattern.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (pdfBytes[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i - dataStart;
                }
            }
            return 0;
        }

        private static List<SayfaBilgi> ObjStmdenSayfaBul(Dictionary<int, string> nesneler, string pdf)
        {
            List<SayfaBilgi> list = new List<SayfaBilgi>();
            foreach (var kv in nesneler)
            {
                if (!SayfaMi(kv.Value)) continue;
                SayfaBilgi s = SayfaParseEt(kv.Key, kv.Value, pdf);
                if (s != null) list.Add(s);
            }
            return list;
        }

        private static List<SayfaBilgi> PagesKidsFallback(string pdf, Dictionary<int, string> objStm)
        {
            List<SayfaBilgi> list = new List<SayfaBilgi>();

            string pagesIcerik = null;
            int pagesObjNo = 0;

            foreach (Match m in Regex.Matches(pdf, @"(\d+)\s+0\s+obj\b"))
            {
                int b = m.Index + m.Length;
                int e = EndobjBul(pdf, b);
                if (e < 0) continue;
                string ic = pdf.Substring(b, e - b);
                if (Regex.IsMatch(ic, @"/Type\s*/Pages"))
                {
                    pagesIcerik = ic;
                    pagesObjNo = int.Parse(m.Groups[1].Value);
                    break;
                }
            }

            if (pagesIcerik == null)
            {
                foreach (var kv in objStm)
                {
                    if (Regex.IsMatch(kv.Value, @"/Type\s*/Pages"))
                    {
                        pagesIcerik = kv.Value;
                        pagesObjNo = kv.Key;
                        break;
                    }
                }
            }

            if (pagesIcerik == null) return list;

            double gW = 595.276, gH = 841.890;
            Match mb = Regex.Match(pagesIcerik, @"/MediaBox\s*\[\s*([\d.]+)\s+([\d.]+)\s+([\d.]+)\s+([\d.]+)\s*\]");
            if (mb.Success)
            {
                gW = double.Parse(mb.Groups[3].Value, CultureInfo.InvariantCulture);
                gH = double.Parse(mb.Groups[4].Value, CultureInfo.InvariantCulture);
            }

            Match km = Regex.Match(pagesIcerik, @"/Kids\s*\[([^\]]+)\]");
            if (!km.Success) return list;

            foreach (Match kr in Regex.Matches(km.Groups[1].Value, @"(\d+)\s+\d+\s+R"))
            {
                int objNo = int.Parse(kr.Groups[1].Value);
                SayfaBilgi s = new SayfaBilgi();
                s.NesneNo = objNo;
                s.Genislik = gW;
                s.Yukseklik = gH;
                s.ParentRef = pagesObjNo + " 0 R";

                string sayfaIcerik = null;
                if (objStm.ContainsKey(objNo))
                    sayfaIcerik = objStm[objNo];
                else
                {
                    Match pm = Regex.Match(pdf, objNo + @"\s+0\s+obj\b");
                    if (pm.Success)
                    {
                        int pb = pm.Index + pm.Length;
                        int pe = EndobjBul(pdf, pb);
                        if (pe > 0) sayfaIcerik = pdf.Substring(pb, pe - pb);
                    }
                }

                if (sayfaIcerik != null)
                    SayfaDetayDoldur(s, sayfaIcerik);

                list.Add(s);
            }
            return list;
        }

        private static bool SayfaMi(string icerik)
        {
            return Regex.IsMatch(icerik, @"/Type\s*/Page(?!\s*s)") &&
                   !Regex.IsMatch(icerik, @"/Type\s*/Pages");
        }

        private static int EndobjBul(string pdf, int basla)
        {
            int pos = basla;
            while (true)
            {
                int endObj = pdf.IndexOf("endobj", pos);
                if (endObj < 0) return -1;

                int streamIdx = pdf.IndexOf("stream", basla);
                if (streamIdx >= 0 && streamIdx < endObj)
                {
                    int endStream = pdf.IndexOf("endstream", streamIdx + 6);
                    if (endStream >= 0 && endStream < endObj)
                        return endObj;
                    if (endStream >= 0)
                    {
                        pos = endStream + 9;
                        continue;
                    }
                }
                return endObj;
            }
        }

        private static SayfaBilgi SayfaParseEt(int objNo, string icerik, string pdf)
        {
            SayfaBilgi s = new SayfaBilgi();
            s.NesneNo = objNo;
            s.RawIcerik = icerik;
            s.Genislik = 595.276;
            s.Yukseklik = 841.890;
            SayfaDetayDoldur(s, icerik);
            return s;
        }

        private static void SayfaDetayDoldur(SayfaBilgi s, string icerik)
        {
            Match mb = Regex.Match(icerik, @"/MediaBox\s*\[\s*([\d.]+)\s+([\d.]+)\s+([\d.]+)\s+([\d.]+)\s*\]");
            if (mb.Success)
            {
                s.Genislik = double.Parse(mb.Groups[3].Value, CultureInfo.InvariantCulture);
                s.Yukseklik = double.Parse(mb.Groups[4].Value, CultureInfo.InvariantCulture);
            }

            Match pr = Regex.Match(icerik, @"/Parent\s+(\d+\s+\d+\s+R)");
            if (pr.Success) s.ParentRef = pr.Groups[1].Value;

            Match ca = Regex.Match(icerik, @"/Contents\s*\[([^\]]+)\]");
            Match ct = Regex.Match(icerik, @"/Contents\s+(\d+)\s+(\d+)\s+R");
            if (ca.Success)
                s.ContentsArray = ca.Groups[1].Value.Trim();
            else if (ct.Success)
                s.ContentsSingle = ct.Groups[1].Value + " " + ct.Groups[2].Value + " R";

            Match ri = Regex.Match(icerik, @"/Resources\s+(\d+\s+\d+\s+R)");
            if (ri.Success)
            {
                s.ResourcesRef = ri.Groups[1].Value;
            }
            else
            {
                int rPos = icerik.IndexOf("/Resources");
                if (rPos >= 0)
                {
                    int dictStart = icerik.IndexOf("<<", rPos);
                    if (dictStart >= 0)
                    {
                        int depth = 0, dictEnd = -1;
                        for (int i = dictStart; i < icerik.Length - 1; i++)
                        {
                            if (icerik[i] == '<' && icerik[i + 1] == '<') { depth++; i++; }
                            else if (icerik[i] == '>' && icerik[i + 1] == '>') { depth--; i++; if (depth == 0) { dictEnd = i + 1; break; } }
                        }
                        if (dictEnd > dictStart)
                            s.ResourcesInline = icerik.Substring(dictStart, dictEnd - dictStart);
                    }
                }
            }

            Match rot = Regex.Match(icerik, @"/Rotate\s+(\d+)");
            if (rot.Success) s.Rotate = rot.Groups[0].Value;
            Match cb = Regex.Match(icerik, @"/CropBox\s*\[[^\]]+\]");
            if (cb.Success) s.CropBox = cb.Groups[0].Value;
        }

        private static void Yaz(MemoryStream ms, string s)
        {
            byte[] b = Latin1.GetBytes(s + "\n");
            ms.Write(b, 0, b.Length);
        }

        private static void YazImageXObject(MemoryStream ms, int no, byte[] data, int w, int h, int maskObjNo)
        {
            Yaz(ms, string.Format("{0} 0 obj", no));
            Yaz(ms, string.Format("<< /Type /XObject /Subtype /Image /Width {0} /Height {1} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /FlateDecode /Length {2} /SMask {3} 0 R >>", w, h, data.Length, maskObjNo));
            Yaz(ms, "stream");
            ms.Write(data, 0, data.Length);
            Yaz(ms, "\nendstream");
            Yaz(ms, "endobj");
        }

        private static void YazMaskXObject(MemoryStream ms, int no, byte[] data, int w, int h)
        {
            Yaz(ms, string.Format("{0} 0 obj", no));
            Yaz(ms, string.Format("<< /Type /XObject /Subtype /Image /Width {0} /Height {1} /ColorSpace /DeviceGray /BitsPerComponent 8 /Filter /FlateDecode /Length {2} >>", w, h, data.Length));
            Yaz(ms, "stream");
            ms.Write(data, 0, data.Length);
            Yaz(ms, "\nendstream");
            Yaz(ms, "endobj");
        }

        private static void YazContentStream(MemoryStream ms, int no, string icerik)
        {
            byte[] d = Latin1.GetBytes(icerik);
            Yaz(ms, string.Format("{0} 0 obj", no));
            Yaz(ms, string.Format("<< /Length {0} >>", d.Length));
            Yaz(ms, "stream");
            ms.Write(d, 0, d.Length);
            Yaz(ms, "\nendstream");
            Yaz(ms, "endobj");
        }

        private static void YazGuncelSayfa(MemoryStream ms, SayfaBilgi s, int overlayNo, int imgObjNo)
        {
            string contentsStr;
            if (!string.IsNullOrEmpty(s.ContentsSingle))
                contentsStr = string.Format("[{0} {1} 0 R]", s.ContentsSingle, overlayNo);
            else if (!string.IsNullOrEmpty(s.ContentsArray))
                contentsStr = string.Format("[{0} {1} 0 R]", s.ContentsArray, overlayNo);
            else
                contentsStr = string.Format("{0} 0 R", overlayNo);

            string resourcesStr;
            if (!string.IsNullOrEmpty(s.ResourcesRef))
            {
                resourcesStr = string.Format("<< /XObject << /SigImg {0} 0 R >> /Font << >> /ProcSet [/PDF /Text /ImageC] >>", imgObjNo);
            }
            else if (!string.IsNullOrEmpty(s.ResourcesInline))
            {
                string res = s.ResourcesInline;
                if (res.Contains("/XObject"))
                {
                    int xoPos = res.IndexOf("/XObject");
                    int xoDict = res.IndexOf("<<", xoPos + 8);
                    if (xoDict >= 0)
                        res = res.Insert(xoDict + 2, string.Format(" /SigImg {0} 0 R ", imgObjNo));
                    resourcesStr = res;
                }
                else
                {
                    int son = res.LastIndexOf(">>");
                    if (son >= 0)
                        res = res.Insert(son, string.Format(" /XObject << /SigImg {0} 0 R >> ", imgObjNo));
                    resourcesStr = res;
                }
            }
            else
            {
                resourcesStr = string.Format("<< /XObject << /SigImg {0} 0 R >> /ProcSet [/PDF /ImageC] >>", imgObjNo);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} 0 obj\n", s.NesneNo);
            sb.Append("<< /Type /Page ");
            if (!string.IsNullOrEmpty(s.ParentRef))
                sb.AppendFormat("/Parent {0} ", s.ParentRef);
            sb.AppendFormat("/MediaBox [0 0 {0} {1}] ",
                s.Genislik.ToString("F2", CultureInfo.InvariantCulture),
                s.Yukseklik.ToString("F2", CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(s.CropBox)) sb.Append(s.CropBox + " ");
            if (!string.IsNullOrEmpty(s.Rotate)) sb.Append(s.Rotate + " ");
            sb.AppendFormat("/Contents {0} ", contentsStr);
            sb.AppendFormat("/Resources {0} ", resourcesStr);
            sb.Append(">>\nendobj\n");

            byte[] d = Latin1.GetBytes(sb.ToString());
            ms.Write(d, 0, d.Length);
        }

        private static void YazXref(MemoryStream ms, Dictionary<int, long> entries)
        {
            Yaz(ms, "xref");
            List<int> keys = new List<int>(entries.Keys);
            keys.Sort();
            int i = 0;
            while (i < keys.Count)
            {
                int start = keys[i];
                int count = 1;
                while (i + count < keys.Count && keys[i + count] == start + count) count++;
                Yaz(ms, string.Format("{0} {1}", start, count));
                for (int j = 0; j < count; j++)
                    Yaz(ms, string.Format("{0:D10} 00000 n ", entries[keys[i + j]]));
                i += count;
            }
        }

        private static void YazTrailer(MemoryStream ms, int size, string rootRef, int prev)
        {
            Yaz(ms, "trailer");
            Yaz(ms, string.Format("<< /Size {0} /Root {1} /Prev {2} >>", size, rootRef, prev));
        }

        private class SayfaBilgi
        {
            public int NesneNo { get; set; }
            public double Genislik { get; set; }
            public double Yukseklik { get; set; }
            public string ParentRef { get; set; }
            public string ContentsSingle { get; set; }
            public string ContentsArray { get; set; }
            public string ResourcesRef { get; set; }
            public string ResourcesInline { get; set; }
            public string RawIcerik { get; set; }
            public string Rotate { get; set; }
            public string CropBox { get; set; }
        }
    }
}

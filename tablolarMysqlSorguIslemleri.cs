using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Net.NetworkInformation;  

namespace socketio_client
{
    class tablolarMysqlSorguIslemleri
    {
        string sorgu = "";
        //int uzakId = 0;
        MySqlCommand komut = new MySqlCommand();
        mySql_islemler tabloIslemler = new mySql_islemler();
        mbox mBox = new mbox();
        constants sabitIslem = new constants();

        #region Temassız Kart satiş işleminin yerel ve web veri tabanlarına  kaydedilmesi
        /// <summary>
        /// string diziye alınmış şekilde gelen veriler "satislar" tablosundaki uygun sutunlara veri tiplerine uygun olarak kaydedilir.
        /// Dönüş değeri olarak tablodaki en son Id yi verir. NULL değer kaydın başarısız olduğunu gösterirve bu durumda -1 döner.
        /// </summary>
        /// <param name="veriler"></param>
        /// <param name="baglanti"></param>
        /// <returns></returns>
 
        public int kartSatisKaydet(string[] veriler, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into satislar (firma_web_id, urun_id, urun_adet, toplam_tutar, odeme_turu, tarih, tarih_zaman, web_kayit,satis_tur,tag_id, aciklama, kupon_seri_no) " +
                   "values (@_firma_web_id, @_urun_id, @_urun_adet, @_toplam_tutar, @_odeme_turu, @_tarih, @_tarih_zaman, @_web_kayit,@_satis_tur,@_tag_id, @_aciklama, @_kupon_seri_no)";

                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into satislar (firma_web_id, urun_id, urun_adet, toplam_tutar, odeme_turu, tarih, tarih_zaman, web_kayit,satis_tur,tag_id, aciklama, kupon_seri_no) " +
                   "values (@_firma_web_id, @_urun_id, @_urun_adet, @_toplam_tutar, @_odeme_turu, @_tarih, @_tarih_zaman, @_web_kayit,@_satis_tur,@_tag_id, @_aciklama, @_kupon_seri_no)";

                komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(sabitIslem.firmaWebIdGetir()));
            komut.Parameters.AddWithValue("@_urun_id", Convert.ToInt32(veriler[eDizi.satislarTab_urun_id]));
            komut.Parameters.AddWithValue("@_urun_adet", Convert.ToInt32(veriler[eDizi.satislarTab_urun_adet]));
            komut.Parameters.AddWithValue("@_toplam_tutar", Convert.ToDecimal(veriler[eDizi.satislarTab_toplam_tutar]));
            komut.Parameters.AddWithValue("@_odeme_turu", veriler[eDizi.satislarTab_odeme_turu]);
            komut.Parameters.AddWithValue("@_tarih", DateTime.Now.ToString("yyyy-MM-dd"));
            komut.Parameters.AddWithValue("@_tarih_zaman", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if(veriler[eDizi.satislarTab_web_kayit]==bool.TrueString)
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit",false);

            komut.Parameters.AddWithValue("@_satis_tur", veriler[eDizi.satislarTab_satis_tur]);
            komut.Parameters.AddWithValue("@_tag_id", veriler[eDizi.satislarTab_tag_id]);
            komut.Parameters.AddWithValue("@_aciklama", veriler[eDizi.satislarTab_aciklama]);
            komut.Parameters.AddWithValue("@_kupon_seri_no", veriler[eDizi.satislarTab_kupon_seri_no]);
            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                    komut.ExecuteNonQuery();

                   // If has last inserted id, add a parameter to hold it.
                    if (komut.LastInsertedId != null) 
                        komut.Parameters.Add(new MySqlParameter("newId", komut.LastInsertedId));
                    else
                    {
                        tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                        return -1;
                    }

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                    return Convert.ToInt32(komut.Parameters["@newId"].Value);
                }

               if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti))
                    {
                        // If has last inserted id, add a parameter to hold it.
                        if (komut.LastInsertedId != null)
                            komut.Parameters.Add(new MySqlParameter("newId", komut.LastInsertedId));
                        else
                        {
                            tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);
                            return -1;
                        }

                        tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);

                        return Convert.ToInt32(komut.Parameters["@newId"].Value);
                    }
                    else return (-1);
                    komut.ExecuteNonQuery();                    

                }
                               
            }
            catch (Exception ex)
            {
                mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return (-1);

            }
            return (-1);
        }//
        #endregion

        #region Son yapılan gün sonunun tarih ve zamanının getirilmesi.
        public DateTime webEnSonGunSonuTarihZamanGetir()
        {
            DateTime tz = DateTime.MinValue;

            DataTable dataTable = tabloIslemler.misTabloListeGetir(constants.UZAK_BAGLANTI, "gunluk_kasa", "islem_tur", "GUN SONU", "firma_web_id",sabitIslem.firmaWebIdGetir(), "and");
            if (dataTable != null)
            {
                //dataTable.DefaultView.Sort = "tarih ASC";
                var dataView = dataTable.DefaultView;
                dataView.Sort = "tarih ASC";
                dataTable = dataView.ToTable();

                if (dataTable.Rows.Count > 0)  //tablo boş değilse
                {
                    tz = Convert.ToDateTime(dataTable.Rows[dataTable.Rows.Count - 1]["tarih"].ToString());//En son devir tarihi
                }
                else //tablo boşsa ilk defa kayıt yapılıyorsa,
                {
                    return DateTime.MinValue; //kasa devir yoksa min deger olsun
                }
                return tz;
            }
            return tz;
           
        }
        public DateTime enSonGunSonuTarihZamanGetir()
        {
            DateTime tz = new DateTime();

            DataTable dt = tabloIslemler.misTabloListeGetir(constants.LOCAL_BAGLANTI, "gunluk_kasa", "islem_tur", "GUN SONU", "", "", "");
            DataView dview = dt.DefaultView;
            dview.Sort = "tarih ASC";
            dt = dview.ToTable();

            if (dt.Rows.Count > 0)  //tablo boş değilse
            {
                tz = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["tarih"].ToString());//En son devir tarihi
            }
            else //tablo boşsa ilk defa kayıt yapılıyorsa,
            {
                return DateTime.MinValue; //kasa devir yoksa min deger olsun
            }

            return tz;

        }
        #endregion

        #region Son yapılan kasa devrin tarih ve zamanının getirilmesi.
        public DateTime enSonKasaDevretTarihZamanGetir()
        {
            DateTime sonKasaDevir = new DateTime();
            DateTime sonGunSonu = enSonGunSonuTarihZamanGetir();

            DataTable dt = tabloIslemler.misTabloListeGetir(constants.LOCAL_BAGLANTI, "gunluk_kasa", "islem_tur", "KASA DEVIR", "", "", "");
            var dataView = dt.DefaultView;
            dataView.Sort = "tarih ASC";
            dt = dataView.ToTable();

            //dt.DefaultView.Sort = "tarih ASC";

            if (dt.Rows.Count > 0)
            {
                sonKasaDevir = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["tarih"].ToString());//En son devir tarihi
                if (DateTime.Compare(sonKasaDevir, sonGunSonu) > 0) return sonKasaDevir;
                else return sonGunSonu;

            }
            else// tablo boşsa ilk defa kasa devir yapılıyorsa
            {
                // return DateTime.MinValue;
                return sonGunSonu;
            }

        }
        #endregion

        #region Yerel ve web veri tabanlarından son gün sonuna göre toplam misafir sayısının getirilmesi.
        /// <summary>
        /// Hata olduğunda -1 değerini döndürür..
        /// </summary>
        /// <param name="baglanti"></param>
        /// <returns></returns>
        public int gunlukMisafirSayisiGetir(string baglanti, int islemTur)  
        {
            int misSayisi=0;
            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (islemTur == constants.ISLEM_GUN_SONU)
                sorgu = "SELECT COUNT(id) From misafirler WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + 
                         " and misafir_bas_zaman >'" + sonGunSonuTarihZaman + "' and misafir_durum!='IPTAL'";
            if (islemTur == constants.ISLEM_KASA_DEVRET)
                sorgu = "SELECT COUNT(id) From misafirler WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + 
                        " and misafir_bas_zaman >'" + sonKasaDevirTarihZaman + "' and misafir_durum!='IPTAL'";
            if(islemTur==constants.ANLIK)
                sorgu = "SELECT COUNT(id) From misafirler WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                         " and misafir_bas_zaman >'" + sonGunSonuTarihZaman + "' and misafir_durum='+1'";

            if (baglanti == constants.LOCAL_BAGLANTI) komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
            if (baglanti == constants.UZAK_BAGLANTI) komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti); 
          
                try
                {
                    if (baglanti == constants.UZAK_BAGLANTI)
                    {     
                        if (tabloIslemler.isUzakBaglanti(constants.PING_HOST) == constants.UZAK_BAGLANTI_VAR)
                        {
                            tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti);

                            object obj = komut.ExecuteScalar();
                            if (obj != null)
                                misSayisi = (Convert.ToInt32(komut.ExecuteScalar()));
                            else misSayisi = 0;

                            tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);

                            return misSayisi;
                        }
                    }
                    if (baglanti == constants.LOCAL_BAGLANTI)
                    {
                        tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                        var obj = komut.ExecuteScalar();

                        if (obj != DBNull.Value)
                             misSayisi = (Convert.ToInt32(obj.ToString()));
                        else misSayisi = 0;

                        tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                        return misSayisi;
                    }
                    
                }
                catch (Exception ex)
                {
                    mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                    return -1;
                }

            return -1;
           
        }//
        #endregion

        public int gunlukKartSatisSayisiGetir(int islemTur, bool nakit_kkarti, bool satis_iade)    //true_false
        {
            int kartSayisi = 0;
            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (satis_iade) //satış
            {
                if(nakit_kkarti) //NaKİT
                {
                    if (islemTur == constants.ISLEM_GUN_SONU)
                        sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                                " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_NAKIT + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "')";

                    if (islemTur == constants.ISLEM_KASA_DEVRET)
                        sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                                " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_NAKIT + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "')";
                }
                else   //KK
                {
                    if (islemTur == constants.ISLEM_GUN_SONU)
                        sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                                " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_KKARTI + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "')";

                    if (islemTur == constants.ISLEM_KASA_DEVRET)
                        sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                                " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_KKARTI + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "')";
                }
               
            }
            else   //iade
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                            " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_IADE+ "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "')";

                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                            " and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_IADE+ "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "')";
            }
            

            komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);

            try
            {
                   tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                    {
                        kartSayisi = (Convert.ToInt32(obj.ToString()));
                    }
                    else kartSayisi = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                    return kartSayisi;
                
            }
            catch (Exception ex)
            {
                // mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return -1;
            }

            return -1;
        }

        public int gunlukIptalSayisiGetir(string baglanti, string tablo, int islemTur)
        {
            int iptalSayisi = 0;
            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (tablo == constants.TABLO_MISAFIR_ODEMELER)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From misafir_odemeler WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                            " and (odeme_turu='" + constants.ISLEM_IPTAL + "' or odeme_turu='" + constants.PICC_ISLEM_IPTAL + "') and tarih_zaman >'" + sonGunSonuTarihZaman + "')";

                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From misafir_odemeler WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                        " and (odeme_turu='" + constants.ISLEM_IPTAL + "' or odeme_turu='" + constants.PICC_ISLEM_IPTAL + "') and tarih_zaman >'" + sonKasaDevirTarihZaman + "')";

            }
            if (tablo == constants.TABLO_SATISLAR)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                            " and (odeme_turu='" + constants.ISLEM_IPTAL + "' or odeme_turu='" + constants.PICC_ISLEM_IPTAL + "') and tarih_zaman >'" + sonGunSonuTarihZaman + "')";

                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From satislar WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                           " and (odeme_turu='" + constants.ISLEM_IPTAL + "' or odeme_turu='" + constants.PICC_ISLEM_IPTAL + "') and tarih_zaman >'" + sonKasaDevirTarihZaman + "')";
            }
            if (tablo == constants.TABLO_MISAFIRLER)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From misafirler WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                            " and (misafir_durum='" + constants.ISLEM_IPTAL + "' or misafir_durum='" + constants.PICC_ISLEM_IPTAL + "') and misafir_bas_zaman >'" + sonGunSonuTarihZaman + "')";

                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From misafirler WHERE (firma_web_id=" + sabitIslem.firmaWebIdGetir() +
                        " and (misafir_durum='" + constants.ISLEM_IPTAL + "' or misafir_durum='" + constants.PICC_ISLEM_IPTAL + "') and misafir_bas_zaman >'" + sonKasaDevirTarihZaman + "')";
            }

            if (baglanti == constants.LOCAL_BAGLANTI)
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
            }
            if (baglanti == constants.UZAK_BAGLANTI)
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti);
            }

            try
            {
                if (baglanti == constants.LOCAL_BAGLANTI)
                {
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                    {
                        iptalSayisi = (Convert.ToInt32(obj.ToString()));
                    }
                    else iptalSayisi = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                    return iptalSayisi;


                }
                if (baglanti == constants.UZAK_BAGLANTI)
                {
                    tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                    {
                        iptalSayisi = (Convert.ToInt32(obj.ToString()));
                    }
                    else iptalSayisi = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);

                    return iptalSayisi;
                }


            }
            catch (Exception ex)
            {
                // mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return -1;
            }

            return -1;
        }

        #region Yerel veri tabanından son gün sonu veya kasa devir işlemine göre kart yada ürün satış(telafi, kupon, vb) sayısını getirir.

        public int localGunlukKartUrunKuponSatisSayisiGetir(string kartUrunKupon, int islemTur)
        {
             int kartUrunKuponSayisi=0;

            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (kartUrunKupon == constants.SATIS_TUR_KART_YUKLEME)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE satis_tur='" + constants.SATIS_TUR_KART_YUKLEME + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "'";
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From satislar WHERE satis_tur='" + constants.SATIS_TUR_KART_YUKLEME + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "'";
            }


            if (kartUrunKupon == constants.SATIS_TUR_URUN)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE satis_tur='" + constants.SATIS_TUR_URUN + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "'";
                 if (islemTur == constants.ISLEM_KASA_DEVRET)
                     sorgu = "SELECT COUNT(id) From satislar WHERE satis_tur='" + constants.SATIS_TUR_URUN + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "'";
            }
            if (kartUrunKupon == constants.ODEME_TUR_KUPON)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE odeme_turu='" + constants.ODEME_TUR_KUPON + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "'";
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From satislar WHERE odeme_turu='" + constants.ODEME_TUR_KUPON + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "'";
            }

            if (kartUrunKupon == constants.ODEME_TUR_TELAFI)
            {
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT COUNT(id) From satislar WHERE odeme_turu='" + constants.ODEME_TUR_TELAFI + "'" + " and tarih_zaman >'" + sonGunSonuTarihZaman + "'";
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT COUNT(id) From satislar WHERE odeme_turu='" + constants.ODEME_TUR_TELAFI + "'" + " and tarih_zaman >'" + sonKasaDevirTarihZaman + "'";
            }
                
            
            komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);

            try
            {
                tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                var obj=komut.ExecuteScalar();

                if (obj != DBNull.Value)
                    kartUrunKuponSayisi = (Convert.ToInt32(komut.ExecuteScalar()));
                else kartUrunKuponSayisi = 0;

                tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                return kartUrunKuponSayisi;

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return -1;
            }
           
            return -1;
        }//
        #endregion

        #region Yerel de "satislar" tablosundan satislara ait ciro, nakit, kredi kartı veya kupon ödeme şekillerinin toplam TL miktarlarını getirir.

        public int kartSatisGetir(int islem)   //Kart satiş sayıyısını getirir
        {
            int kartSatisNakit = 0;
            int kartSatisKkarti = 0;
            int kartSatis = 0;
            int kartIade = 0;

            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (islem == constants.ISLEM_KASA_DEVRET)
            {
                // SELECT COUNT(id) From satislar WHERE firma_web_id=6 and (odeme_turu='KART_DEPOSITO' and tarih_zaman>'2019-06-17 13:20:19')
                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSITO_NAKIT'" + 
                        "and tarih_zaman>'" + sonKasaDevirTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                       kartSatisNakit = (Convert.ToInt32(obj.ToString()));          //Nakit satılan kart sayısı
                    else kartSatisNakit = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {
                    
                    throw;
                }

                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSITO_KKARTI'" +
                        "and tarih_zaman>'" + sonKasaDevirTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                        kartSatisKkarti = (Convert.ToInt32(obj.ToString()));        //Kkartı kart satış sayısı
                    else kartSatisKkarti = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {

                    throw;
                }

                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSIT_IADE'" +
                        "and tarih_zaman>'" + sonKasaDevirTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                        kartIade= (Convert.ToInt32(obj.ToString()));            //iade edilen kart sayısı
                    else kartIade = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {

                    throw;
                }

                kartSatis = (kartSatisNakit+kartSatisKkarti)- kartIade;


            }

            if (islem == constants.ISLEM_GUN_SONU)
            {
                // SELECT COUNT(id) From satislar WHERE firma_web_id=6 and (odeme_turu='KART_DEPOSITO' and tarih_zaman>'2019-06-17 13:20:19')
                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSITO_NAKIT'" +
                        " and tarih_zaman>'" + sonGunSonuTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                        kartSatisNakit = (Convert.ToInt32(obj.ToString()));     //Nakit astılan kart sayısı
                    else kartSatisNakit = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {

                    throw;
                }

                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSITO_KKARTI'" +
                       " and tarih_zaman>'" + sonGunSonuTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                        kartSatisKkarti = (Convert.ToInt32(obj.ToString()));    //Kkartı ile satılan kart sayısı
                    else kartSatisKkarti = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {

                    throw;
                }

                sorgu = "SELECT COUNT(id) From satislar WHERE firma_web_id=" + sabitIslem.firmaWebIdGetir() + " and odeme_turu='KART_DEPOSIT_IADE'" +
                        " and tarih_zaman>'" + sonGunSonuTarihZaman + "'";
                try
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                    var obj = komut.ExecuteScalar();

                    if (obj != DBNull.Value)
                        kartIade = (Convert.ToInt32(obj.ToString()));   // iade edilen kart sayısı
                    else kartIade = 0;

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                }
                catch (Exception)
                {

                    throw;
                }

                kartSatis = (kartSatisNakit + kartSatisKkarti) - kartIade;      //Toplam satılan kart sayısı

            }

            return kartSatis;

        }

        public decimal localGunlukToplamSatisTLGetir(string satisTur, int islemTur)  
        {
            decimal mikTL=0;
            string sonGunSonuTarihZaman = enSonGunSonuTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");
            string sonKasaDevirTarihZaman = enSonKasaDevretTarihZamanGetir().ToString("yyyy-MM-dd HH:mm:ss");

            if (satisTur == constants.ODEME_TUR_NAKIT)
            {
                if(islemTur==constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_NAKIT + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_NAKIT + "'";
            }
            else if (satisTur == constants.ODEME_TUR_KKARTI)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KKARTI + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KKARTI + "'";
            }
            else if (satisTur == constants.ODEME_TUR_KUPON)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KUPON + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KUPON + "'";
            }
            else if (satisTur == constants.SATIS_CIRO)
            {
                bool kuponCiro = false;
                if (tabloIslemler.localTekSatirTabloBilgiGetir("ayarlar", "kupon_ciro") == bool.TrueString) kuponCiro = true;

                if (kuponCiro)
                {
                    if (islemTur == constants.ISLEM_KASA_DEVRET)
                        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "'";
                    if (islemTur == constants.ISLEM_GUN_SONU)
                        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "'";
                }
                else
                {
                    if (islemTur == constants.ISLEM_KASA_DEVRET)
                        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu!='" + constants.ODEME_TUR_KUPON + "'";
                    if (islemTur == constants.ISLEM_GUN_SONU)
                        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu!='" + constants.ODEME_TUR_KUPON + "'";
                }
               
            }
            else if (satisTur == constants.ISLEM_IPTAL)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ISLEM_IPTAL+ "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ISLEM_IPTAL + "'";

            }else if (satisTur == constants.ODEME_TUR_TELAFI)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                   // sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_TELAFI + "'";
                    sorgu = "SELECT SUM(yuklenen_telafi_tl) From telafi_takip WHERE tarih>'" + sonKasaDevirTarihZaman + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    //sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_TELAFI + "'";
                    sorgu = "SELECT SUM(yuklenen_telafi_tl) From telafi_takip WHERE tarih>'" + sonGunSonuTarihZaman + "'";
            }
            else if (satisTur == constants.SATIS_TUR_URUN)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and satis_tur='" + constants.SATIS_TUR_URUN + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and satis_tur='" + constants.SATIS_TUR_URUN + "'";
            }
            else if (satisTur == constants.SATIS_TUR_KART_YUKLEME)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + 
                            "' and satis_tur='" + constants.SATIS_TUR_KART_YUKLEME + "'"+ "and odeme_turu!='TELAFI KONTUR' and odeme_turu!='KART PROMASYON'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman +
                             "' and satis_tur='" + constants.SATIS_TUR_KART_YUKLEME + "'" + "and odeme_turu!='TELAFI KONTUR' and odeme_turu!='KART PROMASYON'"; 
            }
            else if (satisTur == constants.ODEME_TUR_KART_PROMASYON)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_PROMASYON + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_PROMASYON + "'";
            }
            //else if (satisTur == constants.ODEME_TUR_KART_DEPOSITO)
            //{
            //    if (islemTur == constants.ISLEM_KASA_DEVRET)
            //        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO + "'";
            //    if (islemTur == constants.ISLEM_GUN_SONU)
            //        sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO + "'";
            //}
            else if (satisTur == constants.ODEME_TUR_KART_DEPOSITO_NAKIT)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_NAKIT + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_NAKIT + "'";
            }
            else if (satisTur == constants.ODEME_TUR_KART_DEPOSITO_KKARTI)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_KKARTI + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_KKARTI + "'";
            }
            if (satisTur == constants.ODEME_TUR_KART_DEPOSITO_IADE)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_IADE + "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and odeme_turu='" + constants.ODEME_TUR_KART_DEPOSITO_IADE + "'";
            }
            if (satisTur == constants.SATIS_TUR_KART_YUKLEME_GECICI)
            {
                if (islemTur == constants.ISLEM_KASA_DEVRET)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonKasaDevirTarihZaman + "' and satis_tur='" + constants.SATIS_TUR_KART_YUKLEME_GECICI+ "'";
                if (islemTur == constants.ISLEM_GUN_SONU)
                    sorgu = "SELECT SUM(toplam_tutar) From satislar WHERE tarih_zaman>'" + sonGunSonuTarihZaman + "' and satis_tur='" + constants.SATIS_TUR_KART_YUKLEME_GECICI + "'";
            }

            komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);

            try
            {
                tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);

                var obj = komut.ExecuteScalar();

                if (obj != DBNull.Value)
                    mikTL = (Convert.ToDecimal(obj.ToString()));
                else mikTL = 0;

                tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                return mikTL;

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return -1;
            }

            return -1;
        }
        #endregion

        public bool yereldeKartIdGetir(string kartID,string tablo)
        {
            string sorgu = "";
            if (tablo == "abone_takip")
                sorgu = "Select * from abone_takip where (tag_id Like '" + kartID + "') or (tag_id='" + kartID + "')";
            if (tablo == "picc_takip")
                sorgu = "Select * from picc_takip where (tag_id Like '" + kartID + "') or (tag_id='" + kartID + "')";
            if (tablo == "telafi_takip")
                sorgu = "Select * from telafi_takip where (tag_id Like '" + kartID + "') or (tag_id='" + kartID + "')";
            if (tablo == "satislar")
                sorgu = "Select * from satislar where (tag_id Like '" + kartID + "') or (tag_id='" + kartID + "')";

            try
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                var obj = komut.ExecuteScalar();

                if (obj == DBNull.Value || obj==null)
                {
                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                    return false;
                }

            }
            catch (Exception ex)
            {
                //mBox.Show(ex.ToString(), mbox.MSJ_TIP_BILGI);
                return false;
            }
            tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
            return true;
        }

        public bool kartHicKulanilmisMI(string kartID)
        {
            sorgu = "Select * from satislar where tag_id Like '" + kartID + "'";
            try
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                var obj = komut.ExecuteScalar();

                if (obj == DBNull.Value || obj == null)
                {
                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                    return false; // kullanılmamış
                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_BILGI);
                throw;
            }
            tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
            return true; //kullanılmış
            
        }

        public bool kartIslemSil(string kartID, string tablo)
        {
            string sorgu = "";
            if (tablo == "abone_takip")
                sorgu = "DELETE FROM abone_takip WHERE tag_id='" + kartID + "'";
            if (tablo == "picc_takip")
                sorgu = "DELETE FROM picc_takip WHERE tag_id='" + kartID + "'";
            if (tablo == "telafi_takip")
                //sorgu = "DELETE FROM telafi_takip WHERE tag_id='" + kartID + "'";
                sorgu = "UPDATE telafi_takip SET tag_id ='" + "IADE-" + kartID + "'" + "WHERE tag_id='" + kartID + "'";
            if (tablo == "satislar")
                //sorgu = "DELETE FROM telafi_takip WHERE tag_id='" + kartID + "'";
                sorgu= "UPDATE satislar SET tag_id ='"+"IADE-"+kartID +"'" + "WHERE tag_id='"+kartID +"'";

            try
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                komut.ExecuteNonQuery();
                tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                if (tabloIslemler.isUzakBaglanti(constants.PING_HOST) == constants.UZAK_BAGLANTI_VAR)
                {
                    komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti);
                    tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);
                }

                return true;

            }
            catch (Exception ex)
            {
               // mBox.Show(ex.ToString(), mbox.MSJ_TIP_BILGI);
                return false;
                throw;
            }
            return false;
        }

        public int kartIslemKaydet(string[] veriler, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into picc_takip (firma_web_id, tag_id, kontur_miktari, oyuncak_adi, tarih, web_kayit) " +
                   "values (@_firma_web_id, @_tag_id, @_kontur_miktari, @_oyuncak_adi, @_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into picc_takip (firma_web_id, tag_id, kontur_miktari, oyuncak_adi, tarih, web_kayit) " +
                    "values (@_firma_web_id, @_tag_id, @_kontur_miktari, @_oyuncak_adi, @_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti);
            }
            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(sabitIslem.firmaWebIdGetir()));
            komut.Parameters.AddWithValue("@_tag_id", veriler[eDizi.picc_takipTab_tag_id]);
            // komut.Parameters.AddWithValue("@_kontur_miktari", Convert.ToDecimal(veriler[eDizi.picc_takipTab_kontur_miktari]));
            komut.Parameters.AddWithValue("@_kontur_miktari",veriler[eDizi.picc_takipTab_kontur_miktari]);
            komut.Parameters.AddWithValue("@_oyuncak_adi", veriler[eDizi.picc_takipTab_oyuncak_adi]);
            komut.Parameters.AddWithValue("@_tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (veriler[eDizi.picc_takipTab_web_kayit] == bool.TrueString)
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                    komut.ExecuteNonQuery();

                    // If has last inserted id, add a parameter to hold it.
                    if (komut.LastInsertedId != null)
                        komut.Parameters.Add(new MySqlParameter("newId", komut.LastInsertedId));
                    else
                    {
                        tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                        return -1;
                    }

                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);

                    return Convert.ToInt32(komut.Parameters["@newId"].Value);
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    // If has last inserted id, add a parameter to hold it.
                    if (komut.LastInsertedId != null)
                        komut.Parameters.Add(new MySqlParameter("newId", komut.LastInsertedId));
                    else
                    {
                        tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);
                        return -1;
                    }


                    tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);

                    return Convert.ToInt32(komut.Parameters["@newId"].Value);

                }

            }
            catch (Exception ex)
            {
                //mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return (-1);

            }
            return (-1);

         }//

        public string ilave60dkPromFiyatGetir(string islem)
        {
            string sorgu = "SELECT * FROM promasyon WHERE tur='EK 60 DK' and isim='Ek60Dk' and buton='60'";

            DataTable dtProm = tabloIslemler.dataTableGetir(sorgu, tabloIslemler.localBaglanti);

            int idSay = 0;


            if (dtProm != null)// && Convert.ToInt32(grupKisiSayisi)>1)
            {
                if (dtProm.Rows.Count == 0)
                {
                    return null;
                }

                for (int i = 0; i < dtProm.Rows.Count; i++)
                {
                    DateTime bitTar = DateTime.Parse(dtProm.Rows[i]["bitis_tarihi"].ToString());
                    if (DateTime.Compare(bitTar, DateTime.Now) > 0 && dtProm.Rows[i]["onay"].ToString() == bool.TrueString)
                    {
                        if(islem=="fiyat")
                            return dtProm.Rows[i]["fiyat1"].ToString();

                        if (islem == "buton")
                            return dtProm.Rows[i]["buton"].ToString();
                    }
                    
                }// for
            }

            return null;

        }///

    }//class tablolarMysqlSorguIslemleri
}//namespace Oyun_Havuzu_V2

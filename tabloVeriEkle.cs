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

namespace socketio_client
{
    class tabloVeriEkle
    {

        string sorgu = "";
        int uzakId = 0;
        MySqlCommand komut = new MySqlCommand();
        mySql_islemler sqlIslemler = new mySql_islemler();
        mbox mBox = new mbox();
        constants sabitIslem = new constants();

        public DataTable pthTabloGetir()
        {
            DataTable dt = new DataTable();
            string sorgu = "SELECT * FROM nrf_pth_yon WHERE 1";
            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            return dt;
        }
        public bool pthTabloKaydet(byte[] veriler)
        {
            string sorgu = "";
            MySqlCommand komut = new MySqlCommand();

            sorgu = "Insert into nrf_pth_yon(pth1, pth2, pth3, pth4, pth5, pth6,pth7, pth8, pth9, pth10, yon_sure)" +
                          " values(@_pth1, @_pth2, @_pth3, @_pth4, @_pth5, @_pth6, @_pth7, @_pth8, @_pth9, @_pth10, @_yon_sure)";

            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            komut.Parameters.AddWithValue("@_pth1", veriler[0]);
            komut.Parameters.AddWithValue("@_pth2", veriler[1]);
            komut.Parameters.AddWithValue("@_pth3", veriler[2]);
            komut.Parameters.AddWithValue("@_pth4", veriler[3]);
            komut.Parameters.AddWithValue("@_pth5", veriler[4]);
            komut.Parameters.AddWithValue("@_pth6", veriler[5]);
            komut.Parameters.AddWithValue("@_pth7", veriler[6]);
            komut.Parameters.AddWithValue("@_pth8", veriler[7]);
            komut.Parameters.AddWithValue("@_pth9", veriler[8]);
            komut.Parameters.AddWithValue("@_pth10", veriler[9]);
            komut.Parameters.AddWithValue("@_yon_sure", veriler[10]);

            try
            {
                if (sqlIslemler.baglantiAc(sqlIslemler.localBaglanti))
                {
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                    if (komut.LastInsertedId != null)
                        return true;
                    else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                //mBox.Show(baglanti + "\r\n" + ex.ToString(), mbox.MSJ_TIP_HATA);
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                return false;
            }

            return false;
        }//

        public bool truncateTablo(string tablo)
        {
            sorgu = "TRUNCATE TABLE " + tablo;   // tablonun içeriğini tamamen boşaltır.

            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            try
            {
                sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                komut.ExecuteNonQuery();
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                return true;
            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

        }///

        public bool varTablo(string tablo)
        {
            DataTable dt = new DataTable();
            string sorgu = "";

            sorgu = "SHOW TABLES FROM oyun_havuzu";

            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            try
            {
                dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

                foreach (DataRow row in dt.Rows)
                {
                    string tbl = row["Tables_in_oyun_havuzu"].ToString();
                    if (tbl == tablo) return true;

                }

            }
            catch (Exception ex)
            {
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                return false;
            }
            return false;

        }

        public bool kartAboneBul(string kartID, string baglanti, ref string adSoyad, ref string veli, ref string tel)
        {
            DataTable dt = new DataTable();

            string sqlSorgu = "Select ad_soyad, veli_ad, tel from abone_takip where (tag_id Like '" + kartID.Trim() + "') or (tag_id='" + kartID.Trim() + "')";

            try
            {
                if (dt != null)
                {
                    if (baglanti == constants.LOCAL_BAGLANTI)
                    {
                        dt = sqlIslemler.dataTableGetir(sqlSorgu, sqlIslemler.localBaglanti);
                    }
                    if (baglanti == constants.UZAK_BAGLANTI)
                    {
                        dt = sqlIslemler.dataTableGetir(sqlSorgu, sqlIslemler.uzakBaglanti);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        adSoyad = dt.Rows[0]["ad_soyad"].ToString();
                        veli = dt.Rows[0]["veli_ad"].ToString();
                        tel = dt.Rows[0]["tel"].ToString();

                        return true;

                    }
                }
                else
                {
                    adSoyad = "";
                    veli = "";
                    tel = "";
                    return false;
                }

            }
            catch (Exception ex)
            {
                //mbox.Show(ex.ToString(), constants.MSJ_TIP_HATA);
                // MessageBox.Show(ex.ToString(),"OYUN HAVUZU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return false;
        }

        public bool aboneTabloVeriKaydet(string[] veriler, string baglanti)
        {
            MySqlCommand komut = new MySqlCommand();
            string sorgu = "";
            int firmaWebId = Convert.ToInt32(sabitIslem.firmaWebIdGetir());

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into abone_takip (firma_web_id, ad_soyad, veli_ad, tel, abone_tur, abone_bas_bit_tarih, tag_id, kontur_tl, durum, tarih, web_kayit) " +

                      "values (@_firma_web_id, @_ad_soyad, @_veli_ad, @_tel, @_abone_tur, @_abone_bas_bit_tarih, @_tag_id, @_kontur_tl, @_durum, @_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into abone_takip (firma_web_id, ad_soyad, veli_ad, tel, abone_tur, abone_bas_bit_tarih, tag_id, kontur_tl, durum, tarih, web_kayit) " +

                      "values (@_firma_web_id, @_ad_soyad, @_veli_ad, @_tel, @_abone_tur, @_abone_bas_bit_tarih, @_tag_id, @_kontur_tl, @_durum, @_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            }

            if(veriler[eDizi.abone_takip_firma_web_id]==null || veriler[eDizi.abone_takip_firma_web_id] == "")
                komut.Parameters.AddWithValue("@_firma_web_id", firmaWebId);
            else
                komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToUInt32(veriler[eDizi.abone_takip_firma_web_id]));

            komut.Parameters.AddWithValue("@_ad_soyad", veriler[eDizi.abone_takip_ad_soyad]);
            komut.Parameters.AddWithValue("@_veli_ad", veriler[eDizi.abone_takip_veli_ad]);
            komut.Parameters.AddWithValue("@_tel", veriler[eDizi.abone_takip_tel]);
            komut.Parameters.AddWithValue("@_abone_tur", veriler[eDizi.abone_takip_abone_tur]);
            komut.Parameters.AddWithValue("@_abone_bas_bit_tarih", veriler[eDizi.abone_takip_abone_bas_bit_tarih]);
            komut.Parameters.AddWithValue("@_tag_id", veriler[eDizi.abone_takip_tag_id]);
            komut.Parameters.AddWithValue("@_kontur_tl", Convert.ToDecimal(veriler[eDizi.abone_takip_kontur_tl]));
            komut.Parameters.AddWithValue("@_durum", veriler[eDizi.abone_takip_durum]);
            komut.Parameters.AddWithValue("@_tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;

        }

        public int arızaliDegisenKartSayisiGetir(int arizali_degisim)
        {
            int kartSayisi = 0;
            DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
            string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            str += " 23:59:00";
            string sorgu = "";
            if (arizali_degisim == 0)        //arızalı kart
            {
                sorgu = "SELECT COUNT(telafi_islem) From telafi_takip WHERE telafi_islem='ARIZALI KART' and tarih>'" + str + "'";
            }
            if (arizali_degisim == 1)        //arızalı kart
            {
                sorgu = "SELECT COUNT(telafi_islem) From telafi_takip WHERE telafi_islem='KART DEGISIM' and tarih>'" + str + "'";
            }

            //if (baglanti == constants.LOCAL_BAGLANTI) 
            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            try
            {
                sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);

                var obj = komut.ExecuteScalar();

                if (obj != DBNull.Value)
                    kartSayisi = (Convert.ToInt32(obj.ToString()));
                else kartSayisi = 0;

                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                return kartSayisi;

            }
            catch (Exception ex)
            {

                return -1;
            }


            return 0;

        }

        public string nrf_oyuncak_ayar_veri_getir(string o_id, string alan)
        {
            DataTable dt = new DataTable();
            string sorgu = "SELECT * FROM oyuncak_ayar WHERE oyuncak_id='" + o_id + "' order by id asc";

            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[dt.Rows.Count - 1][alan].ToString();
            }

            return "";
        }

        public DataTable nrf_kart_verisi_getir(string tagId, bool tumunuGetir)
        {
            DataTable dt = new DataTable();
            DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
            string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            str += " 23:59:00";
            string sorgu = "";
            if (tumunuGetir)
                sorgu = "SELECT * FROM nrf_oyuncak_kart_takip WHERE kart_id='" + tagId + "' order by id asc";
            else sorgu = "SELECT * FROM nrf_oyuncak_kart_takip WHERE tarih_zaman >'" + str + "' and 	kart_id='" + tagId + "' order by id asc";

            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            return dt;

        }

        public DataTable nrf_oyuncak_kart_veri_getir()
        {
            DataTable dt = new DataTable();
            DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
            string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            str += " 23:59:00";
            string sorgu = "SELECT * FROM nrf_oyuncak_kart_takip WHERE tarih_zaman >'" + str + "' order by id asc";

            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            return dt;

        }

        public bool web_kayit_durum_guncelle(string tablo, int id, bool durum, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE " + tablo + " Set web_kayit=@_web_kayit Where id=@_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                komut.Parameters.AddWithValue("@_id", id);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE " + tablo + " Set web_kayit=@_web_kayit Where id=@_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                komut.Parameters.AddWithValue("@_id", id);

            }
            if (durum) komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }//

        //public void oyuncak_kart_takip_web_kaydet()
        //{
        //    string[] veriler = new string[eDizi.nrf_oyuncak_kart_takip_elemanSayisi];

        //    DataTable dt = new DataTable();
        //    DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
        //    string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
        //    str += " 23:59:00";
        //    string sorgu = "SELECT * FROM nrf_oyuncak_kart_takip WHERE tarih_zaman >'" + str + "' order by id asc";

        //    dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);
        //    if (dt != null)
        //    {
        //        foreach (DataRow row in dt.Rows)// gelen bütün tabloyu incele
        //        {
        //            if (row["web_kayit"].ToString() == bool.FalseString)
        //            {
        //                for (int i = 0; i < dt.Columns.Count - 1; i++)
        //                    veriler[i] = row[i].ToString();

        //                veriler[eDizi.nrf_oyuncak_kart_takip_web_kayit] = bool.TrueString;
        //                if (oyuncak_kart_takip_veri_ekle(veriler, constants.UZAK_BAGLANTI))
        //                    web_kayit_durum_guncelle("nrf_oyuncak_kart_takip", int.Parse(row["id"].ToString()), true, constants.LOCAL_BAGLANTI);
        //                else
        //                {

        //                }
        //            }
        //        }
        //    }

        //}

        public void oyuncak_kart_takip_web_kaydet()
        {
            string[] veriler = new string[eDizi.toys_units_tracker_el_count];

            DataTable dt = new DataTable();
            DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
            string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            str += " 23:59:00";
            string sorgu = "SELECT * FROM nrf_oyuncak_kart_takip WHERE tarih_zaman >'" + str + "' order by id asc";

            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)// gelen bütün tabloyu incele
                {
                    if (row["web_kayit"].ToString() == bool.FalseString)
                    {
                        for (int i = 0; i < dt.Columns.Count - 1; i++)
                            veriler[i] = row[i].ToString();

                        veriler[eDizi.toys_units_tracker_is_web] = bool.TrueString;
                        if (toysUnitsTrackerAddData(veriler, constants.UZAK_BAGLANTI))
                            web_kayit_durum_guncelle("nrf_oyuncak_kart_takip", int.Parse(row["id"].ToString()), true, constants.LOCAL_BAGLANTI);
                        else
                        {

                        }
                    }
                }
            }

        }

        public void getToysDailyData(string deviceId, ref string _dailyTurnOver, ref string _guestCount, ref string _toyPay)
        {
            string sorgu = "";
            DataTable dt = new DataTable();
            DateTime today = DateTime.Now;
            string strToday = today.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            strToday += " 00:00:00";

            //SELECT MIN(date_time), total_token_tl,tag_read, tag_write FROM `toys_units_tracker` WHERE toy_unit_id=34 and `date_time`>'2022-02-08 00:00:00' 

            sorgu = "SELECT * FROM toys_units_tracker WHERE (toy_unit_id='" + deviceId + "') and (date_time >'" + strToday +
                "' and date_time <'" + today.ToString("yyyy-MM-dd HH:mm:ss") + "') ORDER by date_time desc ";

            dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            if (dt != null && dt.Rows.Count>0)
            {
                string firstTotal = dt.Rows[0]["total_token_tl"].ToString();
                string lastTotal = dt.Rows[dt.Rows.Count - 1]["total_token_tl"].ToString();
                decimal toyPay = Convert.ToDecimal(dt.Rows[0]["tag_read"].ToString()) - Convert.ToDecimal(dt.Rows[0]["tag_write"].ToString());

                if(toyPay==0)
                    toyPay = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["tag_read"].ToString()) - Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["tag_write"].ToString());

                _toyPay = toyPay.ToString();

                if (firstTotal == lastTotal)
                {
                    _dailyTurnOver = toyPay.ToString();
                    _guestCount = "1";
                }
                else
                {

                    if (toyPay != 0) {
                        _guestCount = dt.Rows.Count.ToString(); // ((int)((Convert.ToDecimal(firstTotal) - Convert.ToDecimal(lastTotal)) / toyPay)+1).ToString();
                        _dailyTurnOver = (dt.Rows.Count* toyPay).ToString(); // ((Convert.ToDecimal(firstTotal) - Convert.ToDecimal(lastTotal)) + toyPay).ToString();
                    }
                    else
                        _guestCount = "0";
                }

            }
            else
            {
               // _dailyTurnOver
            }
        }



        public bool toysUnitsTrackerAddData(string[] veriler, string baglanti)
        {
            string sorgu = "";
            // int uzakId = 0;
            MySqlCommand komut = new MySqlCommand();

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into  toys_units_tracker (firma_web_id, toy_unit_id, toy_unit_name, tag_id, tag_read, tag_write, operation, total_token_tl, date_time,  msg_id, is_web)" +
                          " values(@_firma_web_id, @_toy_unit_id, @_toy_unit_name, @_tag_id, @_tag_read, @_tag_write, @_operation, @_total_token_tl, @_date_time, @_msg_id, @_is_web)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into  toys_units_tracker (firma_web_id, toy_unit_id, toy_unit_name, tag_id, tag_read, tag_write, operation, total_token_tl, date_time,  msg_id, is_web)" +
                           " values(@_firma_web_id, @_toy_unit_id, @_toy_unit_name, @_tag_id, @_tag_read, @_tag_write, @_operation, @_total_token_tl, @_date_time, @_msg_id, @_is_web)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.toys_units_tracker_firma_web_id]));
            komut.Parameters.AddWithValue("@_toy_unit_id", veriler[eDizi.toys_units_tracker_toy_unit_id]);
            komut.Parameters.AddWithValue("@_toy_unit_name", veriler[eDizi.toys_units_tracker_toy_unit_name]);
            komut.Parameters.AddWithValue("@_tag_id", veriler[eDizi.toys_units_tracker_tag_id]);
            komut.Parameters.AddWithValue("@_tag_read", Convert.ToDecimal(veriler[eDizi.toys_units_tracker_tag_read]));
            komut.Parameters.AddWithValue("@_tag_write", Convert.ToDecimal(veriler[eDizi.toys_units_tracker_tag_write]));
            komut.Parameters.AddWithValue("@_operation", veriler[eDizi.toys_units_tracker_operation]);
            komut.Parameters.AddWithValue("@_total_token_tl", Convert.ToDecimal(veriler[eDizi.toys_units_tracker_total_token_tl]));
            komut.Parameters.AddWithValue("@_date_time", DateTime.Parse(veriler[eDizi.toys_units_tracker_date_time]));
            komut.Parameters.AddWithValue("@_msg_id", Convert.ToInt32(veriler[eDizi.toys_units_tracker_msg_id]));

            if (veriler[eDizi.toys_units_tracker_is_web].Equals(bool.TrueString))
                komut.Parameters.AddWithValue("@_is_web", true);
            else komut.Parameters.AddWithValue("@_is_web", false);


            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.localBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;

                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;
                }

            }
            catch (Exception ex)
            {
                //mBox.Show(baglanti + "\r\n" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;
        }//
        //
        public string oyuncak_adi_getir(string o_id)
        {
            DataTable dt = new DataTable();
            string sqlSorgu = "SELECT * FROM `oyuncak_tanimla` WHERE `oyuncak_id`='" + o_id + "'";
            string retStrVal = "";
            try
            {
                dt = sqlIslemler.dataTableGetir(sqlSorgu, sqlIslemler.localBaglanti);
                retStrVal = dt.Rows[0]["oyuncak_adi"].ToString();
                return retStrVal;
            }
            catch (Exception ex)
            {
                //mbox.Show(ex.ToString(), constants.MSJ_TIP_HATA);
                // MessageBox.Show(ex.ToString(),"OYUN HAVUZU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";

            }
            return retStrVal;
        }

        public bool oyuncak_ayar_veri_ekle(string[] veriler, string baglanti)
        {
            string sorgu = "";
            int uzakId = 0;
            MySqlCommand komut = new MySqlCommand();

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into oyuncak_ayar(firma_web_id, oyuncak_id, oyuncak_adi, jtn_tl, jtn_gecis, no_nc, oku_yenile, reset, tarih_zaman, web_kayit)" +
                          " values(@_firma_web_id, @_oyuncak_id, @_oyuncak_adi, @_jtn_tl, @_jtn_gecis, @_no_nc, @_oku_yenile, @_reset, @_tarih_zaman, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into oyuncak_ayar(firma_web_id, oyuncak_id, oyuncak_adi, jtn_tl, jtn_gecis, no_nc, oku_yenile, reset, tarih_zaman, web_kayit)" +
                          " values(@_firma_web_id, @_oyuncak_id, @_oyuncak_adi, @_jtn_tl,@_jtn_gecis, @_no_nc, @_oku_yenile, @_reset, @_tarih_zaman, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.oyuncak_ayar_firma_web_id]));
            komut.Parameters.AddWithValue("@_oyuncak_id", veriler[eDizi.oyuncak_ayar_oyuncak_id]);
            komut.Parameters.AddWithValue("@_oyuncak_adi", veriler[eDizi.oyuncak_ayar_oyuncak_adi]);
            komut.Parameters.AddWithValue("@_jtn_tl", Convert.ToDecimal(veriler[eDizi.oyuncak_ayar_jtn_tl]));
            komut.Parameters.AddWithValue("@_jtn_gecis", Convert.ToInt32(veriler[eDizi.oyuncak_ayar_jtn_gecis]));
            komut.Parameters.AddWithValue("@_no_nc", veriler[eDizi.oyuncak_ayar_no_nc]);
            komut.Parameters.AddWithValue("@_oku_yenile", Convert.ToInt32(veriler[eDizi.oyuncak_ayar_oku_yenile]));

            if (veriler[eDizi.oyuncak_ayar_reset].Equals(bool.TrueString))
                komut.Parameters.AddWithValue("@_reset", true);
            else komut.Parameters.AddWithValue("@_reset", false);

            komut.Parameters.AddWithValue("@_tarih_zaman", DateTime.Parse(veriler[eDizi.oyuncak_ayar_tarih_zaman]));

            if (veriler[eDizi.oyuncak_ayar_web_kayit].Equals(bool.TrueString))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.localBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;

                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;
                }

            }
            catch (Exception ex)
            {
                mBox.Show(baglanti + "\r\n" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;
        }//

        public DataTable oyuncak_dt_getir()
        {
            string sorgu = "SELECT * FROM oyuncak_tanimla ORDER BY oyuncak_tanimla.ping DESC";
            DataTable dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            return dt;
        }

        public int oyuncak_ping_kontrol()
        {
            int o_sayi = 0;
            bool hata_goster = false;
            string hatalı_oyuncaklar = "";
            DateTime bir_onceki_gun = DateTime.Now.AddDays(-1);
            string str = bir_onceki_gun.ToString("yyyy-MM-dd");        //DateTime.Now.ToString("yyyy-MM-dd");
            str += " 23:59:00";

            string sorgu = "SELECT * FROM  oyuncak_tanimla   WHERE ping>'" + str + "'";

            DataTable dt = sqlIslemler.dataTableGetir(sorgu, sqlIslemler.localBaglanti);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ping_zmn = row["ping"].ToString();
                    DateTime fark = DateTime.Now.AddMinutes((-1) * DateTime.Parse(ping_zmn).Minute);

                    if (fark.Minute > (10)) //30 dk bir hatalı oyuncak varsa listele...
                    {
                        hatalı_oyuncaklar += row["oyuncak_adi"].ToString() + " Son veri gönderme zamanı, " + ping_zmn + "\n";
                        hata_goster = true;

                    }
                    else o_sayi++;


                }
            }
            else return 0;

            //if(hata_goster)
            //    mBox.ShowTimeOut(hatalı_oyuncaklar, mbox.MSJ_TIP_BILGI);

            return o_sayi;


        }//

        public bool oyuncak_ping_guncelle(string o_id, int firma_web, string baglanti)
        {

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE oyuncak_tanimla Set ping=@_ping Where oyuncak_id=@_oyuncak_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                komut.Parameters.AddWithValue("@_oyuncak_id", o_id);  // hex value
                komut.Parameters.AddWithValue("@_ping", DateTime.Now);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE oyuncak_tanimla Set ping=@_ping Where  firma_web_id=@_firma_web_id and oyuncak_id=@_oyuncak_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                komut.Parameters.AddWithValue("@_oyuncak_id", o_id);
                komut.Parameters.AddWithValue("@_firma_web_id", firma_web);
                komut.Parameters.AddWithValue("@_ping", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            }

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                //mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }

        public bool oyuncak_tanimla_güncelle(string[] veriler, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE oyuncak_tanimla Set oyuncak_adi=@_oyuncak_adi, atama=@_atama, " +
                        "web_kayit=@_web_kayit Where oyuncak_id=@_oyuncak_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                komut.Parameters.AddWithValue("@_oyuncak_id", veriler[eDizi.oyuncak_tanimla_oyuncak_id]);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE oyuncak_tanimla Set oyuncak_adi=@_oyuncak_adi, atama=@_atama, " +
                    "web_kayit=@_web_kayit Where (firma_web_id=@_firma_web_id and oyuncak_id=@_oyuncak_id)";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.oyuncak_tanimla_firma_web_id]));
                komut.Parameters.AddWithValue("@_oyuncak_id", veriler[eDizi.oyuncak_tanimla_oyuncak_id]);

            }

            komut.Parameters.AddWithValue("@_oyuncak_adi", veriler[eDizi.oyuncak_tanimla_oyuncak_adi]);

            if (veriler[eDizi.oyuncak_tanimla_atama] == "Evet")
                komut.Parameters.AddWithValue("@_atama", true);
            else komut.Parameters.AddWithValue("@_atama", false);

            if (veriler[eDizi.oyuncak_tanimla_web_kayit].Equals(bool.TrueString))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);


            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;


        }

        public bool kullanicilarVeriGüncelle(string[] veriler, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE kullanicilar Set kullanici_adi=@k_adi, kullanici_sifre=@k_sifre, kullanici_yetki=@k_yetki, " +
                        "sifre_degisim_sayisi=@_sifre_degisim_sayisi, web_kayit=@_web_kayit Where id=@_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                komut.Parameters.AddWithValue("@_id", Convert.ToInt32(veriler[eDizi.kulTab_id]));
            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE kullanicilar Set kullanici_adi=@k_adi, kullanici_sifre=@k_sifre, kullanici_yetki=@k_yetki, " +
                       "sifre_degisim_sayisi=@_sifre_degisim_sayisi, web_kayit=@_web_kayit Where id=@_id";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                komut.Parameters.AddWithValue("@_id", Convert.ToInt32(veriler[eDizi.kulTab_id]));
            }

            komut.Parameters.AddWithValue("@k_adi", veriler[eDizi.kulTab_kulAdi]);
            komut.Parameters.AddWithValue("@k_sifre", veriler[eDizi.kulTab_kulSifre]);
            komut.Parameters.AddWithValue("@k_yetki", veriler[eDizi.kulTab_kulYetki]);
            komut.Parameters.AddWithValue("@_sifre_degisim_sayisi", veriler[eDizi.kulTab_kulSifDegSay]);

            if (veriler[eDizi.kulTab_webKayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }///

        public bool kullanicilarVeriEkle(string[] veriler, string baglanti)
        {

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into kullanicilar (firma_web_id, kullanici_adi, kullanici_sifre, kullanici_yetki, sifre_degisim_sayisi, son_giris_tarihi, web_kayit) " +
                           "values(@_firma_web_id, @k_adi, @k_sifre, @k_yetki, @_sifre_degisim_sayisi, @tarih,@_web_kayit)";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                //komut.Parameters.AddWithValue("@_id", Convert.ToInt32(veriler[kulTab_id])); //ekleme işleminde id kendisi otomatik artar
            }

            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into kullanicilar (firma_web_id, kullanici_adi, kullanici_sifre, kullanici_yetki, sifre_degisim_sayisi, son_giris_tarihi, web_kayit) " +
                           "values(@_firma_web_id, @k_adi, @k_sifre, @k_yetki, @_sifre_degisim_sayisi, @tarih,@_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.kulTab_firmaWebId]));

            komut.Parameters.AddWithValue("@k_adi", veriler[eDizi.kulTab_kulAdi]);
            komut.Parameters.AddWithValue("@k_sifre", veriler[eDizi.kulTab_kulSifre]);
            komut.Parameters.AddWithValue("@_sifre_degisim_sayisi", veriler[eDizi.kulTab_kulSifDegSay]);
            komut.Parameters.AddWithValue("@tarih", DateTime.Parse(veriler[eDizi.kulTab_kulSonGirTar]));
            komut.Parameters.AddWithValue("@k_yetki", veriler[eDizi.kulTab_kulYetki]);

            if (veriler[eDizi.kulTab_webKayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }
                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }////

        //uzak yada local veri tabanına firm abilgi ekler ve id geri döndürür
        public int firmaVeriEkle(string[] veriler, string baglanti)
        {

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into firma_bilgi (id, firma_web_id, firma_adi, firma_adres, ilce, sehir, ad_soyad, tel, mail, ortaklik_durumu, web_kayit) " +
                               "values(@_id, @_firma_web_id, @_firma_adi, @_firma_adres, @_ilce, @_sehir, @_ad_soyad, @_tel, @_mail, @_ortaklik_durumu, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
                komut.Parameters.AddWithValue("@_id", 1);
            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into firma_bilgi (firma_web_id, firma_adi, firma_adres, ilce, sehir, ad_soyad, tel, mail, ortaklik_durumu, web_kayit) " +
                               "values(@_firma_web_id, @_firma_adi, @_firma_adres, @_ilce, @_sehir, @_ad_soyad, @_tel, @_mail, @_ortaklik_durumu, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }
            komut.Parameters.AddWithValue("@_firma_web_id", veriler[eDizi.firmaTab_webId]);//ilk defa kaydeddildiğinde 0 atanıyor çünkü web deki firma ID daha bilinmiyor
            // kayit işleminnden sonra belli olan firma ID getirilip local ve webe güncellenmeli....

            komut.Parameters.AddWithValue("@_firma_adi", veriler[eDizi.firmaTab_adi]);
            komut.Parameters.AddWithValue("@_firma_adres", veriler[eDizi.firmaTab_adres]);
            komut.Parameters.AddWithValue("@_ilce", veriler[eDizi.firmaTab_ilce]);
            komut.Parameters.AddWithValue("@_sehir", veriler[eDizi.firmaTab_sehir]);
            komut.Parameters.AddWithValue("@_ad_soyad", veriler[eDizi.firmaTab_adSoyad]);
            komut.Parameters.AddWithValue("@_tel", veriler[eDizi.firmaTab_tel]);
            komut.Parameters.AddWithValue("@_mail", veriler[eDizi.firmaTab_mail]);

            komut.Parameters.AddWithValue("@_ortaklik_durumu", veriler[eDizi.firmaTab_ortaklik]);

            if (veriler[eDizi.firmaTab_webKayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);

                        return sqlIslemler.idGetir(constants.UZAK_BAGLANTI, "firma_bilgi", "firma_adi", veriler[eDizi.firmaTab_adi], "", "");
                    }
                    else return (-1);

                }
                else
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                    // mBox.Show("Bilgiler kaydedildi.", mbox.MSJ_TIP_BILGI);
                    return (-1);
                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return (-1);
            }
            return (-1);

        }////

        public bool ayarlarVeriEkleGuncelle(string[] veriler, string baglanti, bool ekleGuncelle)
        {
            //ayarlar tablosunda guncelleme için ilk veri bulunması gerekir...

            if (ekleGuncelle)// tabloya ilk veri kaydedilmemişse veri ekle
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sorgu = "Insert into ayarlar (firma_web_id, etiketi_yazdir, gun_sonu_yazdir, kasa_devir_yazdir, max_sifre_deg_sayisi, " +
                  "etiket_sayisi, otomatik_siralama_suresi, otomatik_sirala, ayakkabi_no, izin_suresi, ust_bilgi, alt_bilgi, kasa_nakit_hesabi, web_kayit," +
                  " klavye, islem_sayisi_yaz, kupon_bilgi_yaz, kupon_ciro ) " +

                   "values (@_firma_web_id, @_etiketi_yazdir, @_gun_sonu_yazdir, @_kasa_devir_yazdir, @_max_sifre_deg_sayisi, @_etiket_sayisi," +
                  "@_otomatik_siralama_suresi, @_otomatik_sirala, @_ayakkabi_no, @_izin_suresi, @_ust_bilgi, @_alt_bilgi, @_kasa_nakit_hesabi, @_web_kayit, " +
                  "@_klavye, @_islem_sayisi_yaz, @_kupon_bilgi_yaz, @_kupon_ciro)";

                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
                    komut.Parameters.AddWithValue("@_id", 1);
                    komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.ayarlarTab_firma_web_id]));
                }
                else if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    sorgu = "Insert into ayarlar (firma_web_id, etiketi_yazdir, gun_sonu_yazdir, kasa_devir_yazdir, max_sifre_deg_sayisi, " +
                   "etiket_sayisi, otomatik_siralama_suresi, otomatik_sirala, ayakkabi_no, izin_suresi, ust_bilgi, alt_bilgi, kasa_nakit_hesabi,web_kayit," +
                    "klavye, islem_sayisi_yaz, kupon_bilgi_yaz, kupon_ciro )" +
                    "values (@_firma_web_id, @_etiketi_yazdir, @_gun_sonu_yazdir, @_kasa_devir_yazdir, @_max_sifre_deg_sayisi, @_etiket_sayisi," +
                   "@_otomatik_siralama_suresi, @_otomatik_sirala, @_ayakkabi_no, @_izin_suresi, @_ust_bilgi, @_alt_bilgi, @_kasa_nakit_hesabi, @_web_kayit, " +
                    "@_klavye, @_islem_sayisi_yaz, @_kupon_bilgi_yaz, @_kupon_ciro)";

                    komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
                    komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.ayarlarTab_firma_web_id]));
                }

            }
            else  //tabloda veri varsa güncelleme yap....
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sorgu = "UPDATE ayarlar Set id=@_id, etiketi_yazdir=@_etiketi_yazdir, gun_sonu_yazdir=@_gun_sonu_yazdir, kasa_devir_yazdir=@_kasa_devir_yazdir, " +
                            "max_sifre_deg_sayisi=@_max_sifre_deg_sayisi, etiket_sayisi=@_etiket_sayisi, otomatik_siralama_suresi=@_otomatik_siralama_suresi," +
                            "otomatik_sirala=@_otomatik_sirala, ayakkabi_no=@_ayakkabi_no, izin_suresi=@_izin_suresi, ust_bilgi=@_ust_bilgi, alt_bilgi=@_alt_bilgi," +
                            "kasa_nakit_hesabi=@_kasa_nakit_hesabi, web_kayit=@_web_kayit, klavye=@_klavye, islem_sayisi_yaz=@_islem_sayisi_yaz, kupon_bilgi_yaz=@_kupon_bilgi_yaz, " +
                             "kupon_ciro=@_kupon_ciro Where id=@_id";          //Where id=@i_d

                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                    komut.Parameters.AddWithValue("@_id", 1);

                }
                else if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    sorgu = "UPDATE ayarlar Set id=@_id, etiketi_yazdir=@_etiketi_yazdir, gun_sonu_yazdir=@_gun_sonu_yazdir, kasa_devir_yazdir=@_kasa_devir_yazdir, " +
                             "max_sifre_deg_sayisi=@_max_sifre_deg_sayisi, etiket_sayisi=@_etiket_sayisi, otomatik_siralama_suresi=@_otomatik_siralama_suresi," +
                             "otomatik_sirala=@_otomatik_sirala, ayakkabi_no=@_ayakkabi_no, izin_suresi=@_izin_suresi, ust_bilgi=@_ust_bilgi, alt_bilgi=@_alt_bilgi," +
                             "kasa_nakit_hesabi=@_kasa_nakit_hesabi, web_kayit=@_web_kayit, klavye=@_klavye, islem_sayisi_yaz=@_islem_sayisi_yaz, kupon_bilgi_yaz=@_kupon_bilgi_yaz, " +
                              "kupon_ciro=@_kupon_ciro Where id=@_id";      //Where id=@i_d

                    komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                    uzakId = sqlIslemler.idGetir(constants.UZAK_BAGLANTI, "ayarlar", "firma_web_id", veriler[eDizi.ayarlarTab_firma_web_id], "", "");
                    komut.Parameters.AddWithValue("@_id", uzakId);

                }

            }

            komut.Parameters.AddWithValue("@_etiketi_yazdir", Convert.ToInt32(veriler[eDizi.ayarlarTab_etiket_yazdir]));
            komut.Parameters.AddWithValue("@_gun_sonu_yazdir", Convert.ToInt32(veriler[eDizi.ayarlarTab_gun_sonu_yazdir]));
            komut.Parameters.AddWithValue("@_kasa_devir_yazdir", Convert.ToInt32(veriler[eDizi.ayarlarTab_kasa_devir_yazdir]));
            komut.Parameters.AddWithValue("@_max_sifre_deg_sayisi", Convert.ToInt32(veriler[eDizi.ayarlarTab_max_sifre_deg_sayisi]));
            komut.Parameters.AddWithValue("@_etiket_sayisi", Convert.ToInt32(veriler[eDizi.ayarlarTab_etiket_sayisi]));
            komut.Parameters.AddWithValue("@_otomatik_siralama_suresi", veriler[eDizi.ayarlarTab_otomatik_siralama_suresi]);
            komut.Parameters.AddWithValue("@_otomatik_sirala", Convert.ToInt32(veriler[eDizi.ayarlarTab_otomatik_sirala]));

            if (veriler[eDizi.ayarlarTab_ayakkabi_no].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_ayakkabi_no", true);
            else komut.Parameters.AddWithValue("@_ayakkabi_no", false);

            komut.Parameters.AddWithValue("@_izin_suresi", Convert.ToInt32(veriler[eDizi.ayarlarTab_izin_suresi]));
            komut.Parameters.AddWithValue("@_ust_bilgi", veriler[eDizi.ayarlarTab_ust_bilgi]);
            komut.Parameters.AddWithValue("@_alt_bilgi", veriler[eDizi.ayarlarTab_alt_bilgi]);

            if (veriler[eDizi.ayarlarTab_kasaNakitHesabi].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_kasa_nakit_hesabi", true);
            else komut.Parameters.AddWithValue("@_kasa_nakit_hesabi", false);

            if (veriler[eDizi.ayarlarTab_web_kayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            if (veriler[eDizi.ayarlarTab_klavye].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_klavye", true);
            else komut.Parameters.AddWithValue("@_klavye", false);

            if (veriler[eDizi.ayarlarTab_islemSysYaz].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_islem_sayisi_yaz", true);
            else komut.Parameters.AddWithValue("@_islem_sayisi_yaz", false);

            if (veriler[eDizi.ayarlarTab_kuponBilgiYaz].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_kupon_bilgi_yaz", true);
            else komut.Parameters.AddWithValue("@_kupon_bilgi_yaz", false);

            if (veriler[eDizi.ayarlarTab_kuponCiro].Equals(bool.TrueString)) komut.Parameters.AddWithValue("@_kupon_ciro", true);
            else komut.Parameters.AddWithValue("@_kupon_ciro", false);
            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }
                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }////

        public bool sureButonFiyatlandirmaVeriEkleGuncelle(string[] veriler, string baglanti, bool ekleGuncelle, int talepID)
        {
            if (ekleGuncelle)// tabloya veri ekle
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sorgu = "Insert into sure_buton_isim_fiyatlandirma (id, firma_web_id, buton1_yazi, buton1_fiyat, buton2_yazi, buton2_fiyat," +
                               " buton3_yazi, buton3_fiyat, buton4_yazi, buton4_fiyat, suresiz_isim, suresiz_fiyat, tarife_asimi_sure_dk, tarife_asimi_sure_fiyat," +
                               "talep_tarih, onay, onay_tarih, web_kayit, buton1_fiyat2, buton2_fiyat2, buton3_fiyat2, buton4_fiyat2, suresiz_fiyat2) " +

                               "values(@_id, @_firma_web_id, @_buton1_yazi, @_buton1_fiyat, @_buton2_yazi, @_buton2_fiyat, @_buton3_yazi, @_buton3_fiyat, " +
                               "@_buton4_yazi, @_buton4_fiyat, @_suresiz_isim, @_suresiz_fiyat, @_tarife_asimi_sure_dk, @_tarife_asimi_sure_fiyat, @_talep_tarih," +
                               " @_onay, @_onay_tarih, @_web_kayit, @_buton1_fiyat2, @_buton2_fiyat2, @_buton3_fiyat2, @_buton4_fiyat2, @_suresiz_fiyat2 )";

                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                    komut.Parameters.AddWithValue("@_id", talepID);
                    komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.butSureFiyaTab_firma_web_id]));

                }
                else if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    sorgu = "Insert into sure_buton_isim_fiyatlandirma ( firma_web_id, buton1_yazi, buton1_fiyat, buton2_yazi, buton2_fiyat," +
                               " buton3_yazi, buton3_fiyat, buton4_yazi, buton4_fiyat, suresiz_isim, suresiz_fiyat, tarife_asimi_sure_dk, tarife_asimi_sure_fiyat," +
                               "talep_tarih, onay, onay_tarih, web_kayit, buton1_fiyat2, buton2_fiyat2, buton3_fiyat2, buton4_fiyat2, suresiz_fiyat2) " +

                               "values(@_firma_web_id, @_buton1_yazi, @_buton1_fiyat, @_buton2_yazi, @_buton2_fiyat, @_buton3_yazi, @_buton3_fiyat, " +
                               "@_buton4_yazi, @_buton4_fiyat, @_suresiz_isim, @_suresiz_fiyat, @_tarife_asimi_sure_dk, @_tarife_asimi_sure_fiyat, @_talep_tarih," +
                               " @_onay, @_onay_tarih, @_web_kayit, @_buton1_fiyat2, @_buton2_fiyat2, @_buton3_fiyat2, @_buton4_fiyat2, @_suresiz_fiyat2 )";

                    komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
                    komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.butSureFiyaTab_firma_web_id]));
                }
            }
            else //tablodaki veriyi guncelle
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE sure_buton_isim_fiyatlandirma Set firma_web_id=@_firma_web_id, buton1_yazi=@_buton1_yazi, buton1_fiyat=@_buton1_fiyat, " +
                            "buton2_yazi=@_buton2_yazi, buton2_fiyat=@_buton2_fiyat, buton3_yazi=@_buton3_yazi, buton3_fiyat=@_buton3_fiyat, " +
                            "buton4_yazi=@_buton4_yazi, buton4_fiyat=@_buton4_fiyat, suresiz_isim=@_suresiz_isim, suresiz_fiyat=@_suresiz_fiyat, " +
                            "tarife_asimi_sure_dk=@_tarife_asimi_sure_dk, tarife_asimi_sure_fiyat=@_tarife_asimi_sure_fiyat, talep_tarih=@_talep_tarih, onay=@_onay, " +
                            "onay_tarih=@_onay_tarih, web_kayit=@_web_kayit, buton1_fiyat2=@_buton1_fiyat2, buton2_fiyat2=@_buton2_fiyat2, " +
                            "buton3_fiyat2=@_buton3_fiyat2, buton4_fiyat2=@_buton4_fiyat2, suresiz_fiyat2=@_suresiz_fiyat2 Where id=@_id";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
                komut.Parameters.AddWithValue("@_id", talepID);
                komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.butSureFiyaTab_firma_web_id]));
            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE sure_buton_isim_fiyatlandirma Set firma_web_id=@_firma_web_id, buton1_yazi=@_buton1_yazi, buton1_fiyat=@_buton1_fiyat, " +
                            "buton2_yazi=@_buton2_yazi, buton2_fiyat=@_buton2_fiyat, buton3_yazi=@_buton3_yazi, buton3_fiyat=@_buton3_fiyat, " +
                            "buton4_yazi=@_buton4_yazi, buton4_fiyat=@_buton4_fiyat, suresiz_isim=@_suresiz_isim, suresiz_fiyat=@_suresiz_fiyat, " +
                            "tarife_asimi_sure_dk=@_tarife_asimi_sure_dk, tarife_asimi_sure_fiyat=@_tarife_asimi_sure_fiyat, talep_tarih=@_talep_tarih, onay=@_onay, " +
                            "onay_tarih=@_onay_tarih, web_kayit=@_web_kayit, buton1_fiyat2=@_buton1_fiyat2, buton2_fiyat2=@_buton2_fiyat2, " +
                            "buton3_fiyat2=@_buton3_fiyat2, buton4_fiyat2=@_buton4_fiyat2, suresiz_fiyat2=@_suresiz_fiyat2 Where id=@_id";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

                uzakId = sqlIslemler.idGetir(constants.UZAK_BAGLANTI, "sure_buton_isim_fiyatlandirma", "firma_web_id", veriler[eDizi.butSureFiyaTab_firma_web_id], "", "");
                komut.Parameters.AddWithValue("@_id", uzakId);
                komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.butSureFiyaTab_firma_web_id]));
            }

            komut.Parameters.AddWithValue("@_buton1_yazi", veriler[eDizi.butSureFiyaTab_buton1_yazi]);
            komut.Parameters.AddWithValue("@_buton1_fiyat", veriler[eDizi.butSureFiyaTab_buton1_fiyat]);
            komut.Parameters.AddWithValue("@_buton1_fiyat2", veriler[eDizi.butSureFiyaTab_buton1_fiyat2]);

            komut.Parameters.AddWithValue("@_buton2_yazi", veriler[eDizi.butSureFiyaTab_buton2_yazi]);
            komut.Parameters.AddWithValue("@_buton2_fiyat", veriler[eDizi.butSureFiyaTab_buton2_fiyat]);
            komut.Parameters.AddWithValue("@_buton2_fiyat2", veriler[eDizi.butSureFiyaTab_buton2_fiyat2]);

            komut.Parameters.AddWithValue("@_buton3_yazi", veriler[eDizi.butSureFiyaTab_buton3_yazi]);
            komut.Parameters.AddWithValue("@_buton3_fiyat", veriler[eDizi.butSureFiyaTab_buton3_fiyat]);
            komut.Parameters.AddWithValue("@_buton3_fiyat2", veriler[eDizi.butSureFiyaTab_buton3_fiyat2]);

            komut.Parameters.AddWithValue("@_buton4_yazi", veriler[eDizi.butSureFiyaTab_buton4_yazi]);
            komut.Parameters.AddWithValue("@_buton4_fiyat", veriler[eDizi.butSureFiyaTab_buton4_fiyat]);
            komut.Parameters.AddWithValue("@_buton4_fiyat2", veriler[eDizi.butSureFiyaTab_buton4_fiyat2]);

            komut.Parameters.AddWithValue("@_suresiz_isim", veriler[eDizi.butSureFiyaTab_suresiz_isim]);
            komut.Parameters.AddWithValue("@_suresiz_fiyat", veriler[eDizi.butSureFiyaTab_suresiz_fiyat]);
            komut.Parameters.AddWithValue("@_suresiz_fiyat2", veriler[eDizi.butSureFiyaTab_suresiz_fiyat2]);


            komut.Parameters.AddWithValue("@_tarife_asimi_sure_dk", veriler[eDizi.butSureFiyaTab_tarife_asimi_sure_dk]);
            komut.Parameters.AddWithValue("@_tarife_asimi_sure_fiyat", Convert.ToDecimal(veriler[eDizi.butSureFiyaTab_tarife_asimi_sure_fiyat]));

            komut.Parameters.AddWithValue("@_talep_tarih", DateTime.Parse(veriler[eDizi.butSureFiyaTab_talep_tarih]));

            if (veriler[eDizi.butSureFiyaTab_onay].Equals(constants.TRUE_STR) || veriler[eDizi.butSureFiyaTab_onay].Equals(bool.TrueString) ||
                        veriler[eDizi.butSureFiyaTab_onay].Equals("1,00"))
                komut.Parameters.AddWithValue("@_onay", true);
            else komut.Parameters.AddWithValue("@_onay", false);

            komut.Parameters.AddWithValue("@_onay_tarih", DateTime.Parse(veriler[eDizi.butSureFiyaTab_onay_tarih]));

            if (veriler[eDizi.butSureFiyaTab_web_kayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);


            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }///

        public bool sureFiyatLandirmaTalepSil(int talepId)
        {
            sorgu = "Delete From sure_buton_isim_fiyatlandirma Where id=@_id";
            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            komut.Parameters.AddWithValue("@_id", talepId);

            try
            {
                sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                komut.ExecuteNonQuery();
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                return true;

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

        }//
        /// <summary>
        /// web de onaylanmış fiayt talebi varsa bilgileri alır local e  Id=1 olarak kaydeder ve true döndürürr..
        /// değilse false 
        /// </summary>
        /// <param name="talepId"></param>
        /// <returns></returns>
        public bool fiyatTalepKontrol(int talepId)
        {
            if (sqlIslemler.isUzakBaglanti(constants.PING_HOST) == constants.UZAK_BAGLANTI_VAR)
            {
                string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");

                DataTable dtUzak = sqlIslemler.misTabloListeGetir(constants.UZAK_BAGLANTI, "sure_buton_isim_fiyatlandirma", "firma_web_id", firmaWebId, "", "", "");
                DataTable dtLocal = sqlIslemler.butunVerileriSiraliGetir("sure_buton_isim_fiyatlandirma", constants.LOCAL_BAGLANTI);

                string[] veriler = new string[dtLocal.Columns.Count];

                if (dtUzak != null)// Uzak bağlantı yapıldı ve veriler getirildi ise;
                {
                    for (int i = 0; i < dtUzak.Rows.Count; i++)
                    {
                        if (dtUzak.Rows[i]["talep_tarih"].ToString().Equals(dtLocal.Rows[talepId - 1]["talep_tarih"].ToString()))
                        {   //local ve web talep tarihleri aynı ise;
                            if (dtUzak.Rows[i]["onay"].ToString().Equals(bool.TrueString))  //web de fiyat talebi onaylanmış ise;
                            {
                                for (int j = 0; j < dtUzak.Columns.Count; j++)
                                    veriler[j] = dtUzak.Rows[i][j].ToString();  // onaylanmış ise talep verilerini al...
                            }
                            else return false;  // fiyat taelbi onaylanmamış ise

                            if (talepId == constants.SURE_FIYAT_YENI_TALEP)  //eğer yeni fiyat talebi varsa
                            {
                                if (sureFiyatLandirmaTalepSil(talepId))  //localdeki ilk fiyatı sil.. Id=1 olan
                                {
                                    veriler[eDizi.butSureFiyaTab_web_kayit] = constants.TRUE_STR;

                                    if (sureButonFiyatlandirmaVeriEkleGuncelle(veriler, constants.LOCAL_BAGLANTI, constants.SURE_FIYAT_GUNCELLEME_ISLEMI, constants.SURE_FIYAT_ILK_KAYIT))
                                    {  //Her zaman ilk kayıt güncellenir tablo da sadece güncel 1 kayıt bulunur.
                                        // mBox.Show("Fiyat talebiniz onaylanmış ve güncellenmiştir", mbox.MSJ_TIP_BILGI);
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                veriler[eDizi.butSureFiyaTab_web_kayit] = constants.TRUE_STR;

                                if (sureButonFiyatlandirmaVeriEkleGuncelle(veriler, constants.LOCAL_BAGLANTI, constants.SURE_FIYAT_GUNCELLEME_ISLEMI, constants.SURE_FIYAT_ILK_KAYIT))
                                {
                                    //mBox.Show("Fiyat talebiniz onaylanmış ve güncellenmiştir", mbox.MSJ_TIP_BILGI);
                                    return true;
                                }
                            }

                        }///talep trih
                    }///for 
                }
            }

            return false;
        }///

        public bool promasyonVeriKaydet(DataGridView dgw, int row, int firmaWebId, bool webKayit, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into promasyon (firma_web_id, isim, tur, buton, fiyat1, gecerli_saat, fiyat2, baslangic_tarihi, bitis_tarihi, talep_tarih, onay, web_kayit) " +

                      "values (@_firma_web_id, @_isim, @_tur, @_buton, @_fiyat1, @_gecerli_saat, @_fiyat2, @_baslangic_tarihi, @_bitis_tarihi, @_talep_tarih, @_onay, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                //komut.Parameters.AddWithValue("@_id", talepID);
                komut.Parameters.AddWithValue("@_firma_web_id", firmaWebId);

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into promasyon (firma_web_id, isim, tur, buton, fiyat1, gecerli_saat, fiyat2, baslangic_tarihi, bitis_tarihi, talep_tarih, onay, web_kayit) " +

                       "values (@_firma_web_id, @_isim, @_tur, @_buton, @_fiyat1, @_gecerli_saat, @_fiyat2, @_baslangic_tarihi, @_bitis_tarihi, @_talep_tarih, @_onay, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
                komut.Parameters.AddWithValue("@_firma_web_id", firmaWebId);
            }

            komut.Parameters.AddWithValue("@_isim", dgw.Rows[row].Cells["isim"].Value.ToString());
            komut.Parameters.AddWithValue("@_tur", dgw.Rows[row].Cells["tur"].Value.ToString());
            komut.Parameters.AddWithValue("@_buton", dgw.Rows[row].Cells["buton"].Value.ToString());
            komut.Parameters.AddWithValue("@_fiyat1", Convert.ToDecimal(dgw.Rows[row].Cells["fiyat1"].Value.ToString()));
            string saat = dgw.Rows[row].Cells["gecerli_saat"].Value.ToString();
            saat = saat.Trim();
            if (saat != ":")
                komut.Parameters.AddWithValue("@_gecerli_saat", Convert.ToDateTime(dgw.Rows[row].Cells["gecerli_saat"].Value.ToString()));
            else komut.Parameters.AddWithValue("@_gecerli_saat", Convert.ToDateTime("00:00"));
            komut.Parameters.AddWithValue("@_fiyat2", Convert.ToDecimal(dgw.Rows[row].Cells["fiyat2"].Value.ToString()));

            komut.Parameters.AddWithValue("@_baslangic_tarihi", DateTime.Parse(dgw.Rows[row].Cells["baslangic_tarihi"].Value.ToString()));
            komut.Parameters.AddWithValue("@_bitis_tarihi", DateTime.Parse(dgw.Rows[row].Cells["bitis_tarihi"].Value.ToString()));
            komut.Parameters.AddWithValue("@_talep_tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            komut.Parameters.AddWithValue("@_onay", false);
            komut.Parameters.AddWithValue("@_web_kayit", webKayit);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

            return false;
        }///

        public bool promasyonTalepKontrol()
        {

            //if (sqlIslemler.isUzakBaglanti(constants.PING_HOST) == constants.UZAK_BAGLANTI_VAR)
            //{
            //    string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");

            //    DataTable dtUzak = sqlIslemler.misTabloListeGetir(constants.UZAK_BAGLANTI, "promasyon", "firma_web_id", firmaWebId, "", "", "");
            //    DataTable dtLocal = sqlIslemler.butunVerileriSiraliGetir("promasyon", constants.LOCAL_BAGLANTI);

            //    string[] veriler = new string[dtLocal.Columns.Count];
            //    if (dtUzak != null)
            //    {
            //        for (int i = 0; i < dtUzak.Rows.Count; i++)
            //        {
            //            if (dtUzak.Rows[i]["talep_tarih"].ToString().Equals(dtLocal.Rows[0]["talep_tarih"].ToString()))
            //            {
            //                DateTime promBasTar = DateTime.Parse(dtUzak.Rows[i]["baslangic_tarihi"].ToString());
            //                DateTime promBitTar = DateTime.Parse(dtUzak.Rows[i]["bitis_tarihi"].ToString());

            //                if (promBitTar > promBasTar)
            //                {
            //                    if (dtUzak.Rows[i]["onay"].ToString().Equals(bool.TrueString))
            //                    {
            //                        for (int j = 0; j < dtUzak.Columns.Count; j++)
            //                            veriler[j] = dtUzak.Rows[i][j].ToString();

            //                        if (promasyonTablosuSil())
            //                        {
            //                            if (promasyonVeriEkle(veriler, constants.LOCAL_BAGLANTI))
            //                            {
            //                                mBox.Show("Promasyon talebiniz onaylanmıştır.", mbox.MSJ_TIP_BILGI);
            //                                return true;
            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //        }//for
            //    }

            //}

            return false;
        }////

        public bool promasyonTablosuSil()
        {
            //sorgu = "Delete * From promasyon "; 
            sorgu = "TRUNCATE TABLE promasyon ";   // tablonun içeriğini tamamen boşaltır.

            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            try
            {
                sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                komut.ExecuteNonQuery();
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                return true;
            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

        }///

        public bool kulllaniciTabloKulaniciSil(string kulAdi, string baglanti)
        {
            bool islem = false;
            DataTable dtKul = sqlIslemler.misTabloListeGetir(baglanti, "kullanicilar", "kullanici_adi", kulAdi, "", "", "");

            for (int i = 0; i < dtKul.Rows.Count; i++)
            {
                if (kullaniciTabloIdSil(Convert.ToInt32(dtKul.Rows[i]["id"].ToString()), baglanti)) islem = true;
                else islem = false;
            }
            return islem;

        }//

        public bool kullaniciTabloIdSil(int silId, string baglanti)
        {
            sorgu = "Delete From kullanicilar Where id=@_id";

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            if (baglanti.Equals(constants.UZAK_BAGLANTI))
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            komut.Parameters.AddWithValue("@_id", silId);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }////

        public long misafirVeriKaydet(string[] veriler, string baglanti, string islemTip, string ekSureTalebi, string ekSureMisKalanSure)
        {
            string misBasZaman = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string misBitZaman = "";

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into misafirler (firma_web_id, misafir_adi, misafir_veli, misafir_veli_tel, misafir_sure, misafir_ek_sure," +
                            " misafir_tarih, misafir_bas_zaman, misafir_bit_zaman, ayakkabilik_no, izin_adi, misafir_aciklama, misafir_durum, web_kayit,soft_alan) " +

                            "values(@_firma_web_id, @_misafir_adi, @_misafir_veli, @_misafir_veli_tel, @_misafir_sure, @_misafir_ek_sure, " +
                            "@_misafir_tarih, @_misafir_bas_zaman, @_misafir_bit_zaman, @_ayakkabilik_no, @_izin_adi, @_misafir_aciklama, @_misafir_durum, @_web_kayit, @_soft_alan)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into misafirler (firma_web_id, misafir_adi, misafir_veli, misafir_veli_tel, misafir_sure, misafir_ek_sure," +
                            " misafir_tarih, misafir_bas_zaman, misafir_bit_zaman, ayakkabilik_no, izin_adi, misafir_aciklama, misafir_durum, web_kayit) " +

                            "values(@_firma_web_id, @_misafir_adi, @_misafir_veli, @_misafir_veli_tel, @_misafir_sure, @_misafir_ek_sure, " +
                            "@_misafir_tarih, @_misafir_bas_zaman, @_misafir_bit_zaman, @_ayakkabilik_no, @_izin_adi, @_misafir_aciklama, @_misafir_durum, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.misafirlerTab_firma_web_id]));
            komut.Parameters.AddWithValue("@_misafir_adi", veriler[eDizi.misafirlerTab_misafir_adi]);
            komut.Parameters.AddWithValue("@_misafir_veli", veriler[eDizi.misafirlerTab_misafir_veli]);
            komut.Parameters.AddWithValue("@_misafir_veli_tel", veriler[eDizi.misafirlerTab_misafir_veli_tel]);
            komut.Parameters.AddWithValue("@_misafir_sure", Convert.ToInt32(veriler[eDizi.misafirlerTab_misafir_sure]));

            if (islemTip == constants.ISLEM_SURELI || islemTip == constants.ISLEM_SURESIZ_PESIN)
            {
                if (ekSureTalebi == constants.ISLEM_EK_SURE)
                {

                    komut.Parameters.AddWithValue("@_misafir_ek_sure", veriler[eDizi.misafirlerTab_misafir_ek_sure]);

                    komut.Parameters.AddWithValue("@_misafir_tarih", DateTime.Now.ToString("yyyy-MM-dd"));     //ToString("yyyy-MM-dd HH:mm:ss"))
                    // misBitZaman=DateTime.Now.AddMinutes(Convert.ToDouble(veriler[eDizi.misafirlerTab_misafir_sure]) + Convert.ToInt32(ekSureMisKalanSure)).ToString("yyyy-MM-dd HH:mm:ss");

                    if (islemTip == constants.ISLEM_SURESIZ_PESIN)
                    {
                        komut.Parameters.AddWithValue("@_misafir_bit_zaman", misBasZaman);
                    }
                    else komut.Parameters.AddWithValue("@_misafir_bit_zaman", veriler[eDizi.misafirlerTab_misafir_bit_zaman]);

                    komut.Parameters.AddWithValue("@_misafir_bas_zaman", misBasZaman);

                }
                else
                {
                    komut.Parameters.AddWithValue("@_misafir_ek_sure", "");
                    komut.Parameters.AddWithValue("@_misafir_tarih", DateTime.Now.ToString("yyyy-MM-dd"));     //ToString("yyyy-MM-dd HH:mm:ss"))

                    misBasZaman = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    misBitZaman = DateTime.Now.AddMinutes(Convert.ToDouble(veriler[eDizi.misafirlerTab_misafir_sure])).ToString("yyyy-MM-dd HH:mm:ss");

                    komut.Parameters.AddWithValue("@_misafir_bas_zaman", misBasZaman);
                    komut.Parameters.AddWithValue("@_misafir_bit_zaman", misBitZaman);

                }

            }//sureli işlem

            if (islemTip == constants.ISLEM_SURESIZ)
            {
                komut.Parameters.AddWithValue("@_misafir_ek_sure", "");
                komut.Parameters.AddWithValue("@_misafir_tarih", DateTime.Now.ToString("yyyy-MM-dd "));     //ToString("yyyy-MM-dd HH:mm:ss"))

                misBasZaman = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                komut.Parameters.AddWithValue("@_misafir_bas_zaman", misBasZaman);
                misBitZaman = "...";
                komut.Parameters.AddWithValue("@_misafir_bit_zaman", DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (veriler[eDizi.misafirlerTab_misafir_ayakkabilik_no] == "")
            {
                veriler[eDizi.misafirlerTab_misafir_ayakkabilik_no] = "0";
            }

            komut.Parameters.AddWithValue("@_ayakkabilik_no", Convert.ToInt32(veriler[eDizi.misafirlerTab_misafir_ayakkabilik_no]));
            komut.Parameters.AddWithValue("@_izin_adi", veriler[eDizi.misafirlerTab_misafir_izin_adi]);
            komut.Parameters.AddWithValue("@_misafir_aciklama", islemTip);
            komut.Parameters.AddWithValue("@_misafir_durum", constants.SURESI_BASLADI);

            ///web durumu sorgulanacak....
            if (veriler[eDizi.misafirlerTab_web_kayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            komut.Parameters.AddWithValue("@_soft_alan", veriler[eDizi.misafirlerTab_soft_alan]);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                    if (komut.LastInsertedId != null)
                        return komut.LastInsertedId;
                    else return 0;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);

                        if (komut.LastInsertedId != null)
                            return komut.LastInsertedId;
                        else return 0;
                    }
                    else return 0;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return 0;
            }
            return 0;
        }////

        public bool misafirOdemeKaydet(string[] veriler, string baglanti, string odemeTur)
        {

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into misafir_odemeler (firma_web_id, misafir_id, tutar, tarih,tarih_zaman, odeme_turu, web_kayit, kupon_seri_no, indirim_tur, grup_kardes_id)" +
                           " values(@_firma_web_id, @_misafir_id, @_tutar, @_tarih, @_tarih_zaman, @_odeme_turu, @_web_kayit, @_kupon_seri_no, @_indirim_tur, @_grup_kardes_id)";

                #region AUTH SFR ALANI

                string ok_sfr = "0";//sabitIslem.localPiccAuthSifreOku();// sabitIslem.piccAuthSifreOku();

                if (ok_sfr == "1")
                {
                    sorgu = "Insert into misafir_odemeler (id,firma_web_id, misafir_id, tutar, tarih,tarih_zaman, odeme_turu, web_kayit, kupon_seri_no, indirim_tur, grup_kardes_id)" +
                           " values(@_id,@_firma_web_id, @_misafir_id, @_tutar, @_tarih, @_tarih_zaman, @_odeme_turu, @_web_kayit, @_kupon_seri_no, @_indirim_tur, @_grup_kardes_id)";
                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

                    string son = sabitIslem.misOdemlerTabloLocalEnsonIdGetir();
                    komut.Parameters.AddWithValue("@_id", Convert.ToInt32(son));

                }
                else if (ok_sfr == "2")
                {
                    sorgu = "Insert into misafir_odemeler (id,firma_web_id, misafir_id, tutar, tarih,tarih_zaman, odeme_turu, web_kayit, kupon_seri_no, indirim_tur, grup_kardes_id)" +
                           " values(@_id,@_firma_web_id, @_misafir_id, @_tutar, @_tarih, @_tarih_zaman, @_odeme_turu, @_web_kayit, @_kupon_seri_no, @_indirim_tur, @_grup_kardes_id)";
                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
                }
                else
                    #endregion

                    komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into misafir_odemeler (firma_web_id, misafir_id, tutar, tarih,tarih_zaman, odeme_turu, web_kayit, kupon_seri_no, indirim_tur, grup_kardes_id)" +
                           " values(@_firma_web_id, @_misafir_id, @_tutar, @_tarih, @_tarih_zaman, @_odeme_turu, @_web_kayit, @_kupon_seri_no, @_indirim_tur, @_grup_kardes_id)";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }



            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.misOdemeTab_firma_web_id]));
            komut.Parameters.AddWithValue("@_misafir_id", Convert.ToInt32(veriler[eDizi.misOdemeTab_misafir_id]));
            komut.Parameters.AddWithValue("@_tutar", Convert.ToDecimal(veriler[eDizi.misOdemeTab_tutar]));

            komut.Parameters.AddWithValue("@_tarih", DateTime.Now.ToString("yyyy-MM-dd"));
            komut.Parameters.AddWithValue("@_tarih_zaman", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (odemeTur.Equals(constants.ODEME_TUR_KKARTI)) komut.Parameters.AddWithValue("@_odeme_turu", constants.ODEME_TUR_KKARTI);
            if (odemeTur.Equals(constants.ODEME_TUR_NAKIT)) komut.Parameters.AddWithValue("@_odeme_turu", constants.ODEME_TUR_NAKIT);
            if (odemeTur.Equals(constants.ODEME_TUR_KUPON)) komut.Parameters.AddWithValue("@_odeme_turu", constants.ODEME_TUR_KUPON);
            if (odemeTur.Equals(constants.ODEME_TUR_PICC)) komut.Parameters.AddWithValue("@_odeme_turu", constants.ODEME_TUR_PICC);

            if (veriler[eDizi.misOdemeTab_web_kayit].Equals(constants.TRUE_STR))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            komut.Parameters.AddWithValue("@_kupon_seri_no", veriler[eDizi.misOdemeTab_kupon_seri_no]);
            komut.Parameters.AddWithValue("@_indirim_tur", veriler[eDizi.misOdemeTab_indirim_tur]);
            komut.Parameters.AddWithValue("@_grup_kardes_id", veriler[eDizi.misOdemeTab_grup_kardes_id]);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.localBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;

                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);

                        if (komut.LastInsertedId != null)

                            return true;

                        else return false;

                    }
                    else return false;
                }

            }
            catch (Exception ex)
            {
                mBox.Show(baglanti + "\r\n" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }

        public bool misafirIptalKaydet(string[] veriler, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into iptal_takip (firma_web_id, tarih_saat, iptal_eden, islem_turu, aciklama, web_kayit)" +
                         " values(@_firma_web_id, @_tarih_saat, @_iptal_eden, @_islem_turu, @_aciklama, @_web_kayit)";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into iptal_takip (firma_web_id, tarih_saat, iptal_eden, islem_turu, aciklama, web_kayit)" +
                         " values(@_firma_web_id, @_tarih_saat, @_iptal_eden, @_islem_turu, @_aciklama, @_web_kayit)";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[eDizi.iptalTab_firma_web_id]));
            komut.Parameters.AddWithValue("@_tarih_saat", DateTime.Parse(veriler[eDizi.iptalTab_tarih_saat]));
            komut.Parameters.AddWithValue("@_iptal_eden", veriler[eDizi.iptalTab_iptal_eden]);
            komut.Parameters.AddWithValue("@_islem_turu", veriler[eDizi.iptalTab_islem_turu]);
            komut.Parameters.AddWithValue("@_aciklama", veriler[eDizi.iptalTab_aciklama]);

            if (veriler[eDizi.iptalTab_web_kayit].Equals(constants.TRUE_STR) || veriler[eDizi.iptalTab_web_kayit].Equals(bool.TrueString))
                komut.Parameters.AddWithValue("@_web_kayit", true);
            else komut.Parameters.AddWithValue("@_web_kayit", false);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;

        }

        public bool kasaCikisVeriKaydet(DataGridView dgw, int row, int firmaWebId, bool webKayit, string baglanti)
        {
            string bankaAdi = dgw.Rows[row].Cells["bankaAdi"].Value.ToString();
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into kasa_cikis (firma_web_id, cikis_adi, cikis_tur_id, cikis_tutar, banka_id, banka_islem_tarih, tarih, tarih_zaman, 	islem_yapan, islem_not, web_kayit) " +
                     "values (@_firma_web_id, @_cikis_adi, @_cikis_tur_id, @_cikis_tutar, @_banka_id, @_banka_islem_tarih, @_tarih, @_tarih_zaman, @_islem_yapan, @_islem_not, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into kasa_cikis (firma_web_id, cikis_adi, cikis_tur_id, cikis_tutar, banka_id, banka_islem_tarih, tarih, tarih_zaman, 	islem_yapan, islem_not, web_kayit) " +
                     "values (@_firma_web_id, @_cikis_adi, @_cikis_tur_id, @_cikis_tutar, @_banka_id, @_banka_islem_tarih, @_tarih, @_tarih_zaman, @_islem_yapan, @_islem_not, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", firmaWebId);
            komut.Parameters.AddWithValue("@_cikis_adi", dgw.Rows[row].Cells["cikisTur"].Value.ToString());
            komut.Parameters.AddWithValue("@_cikis_tur_id", sqlIslemler.idGetir(constants.LOCAL_BAGLANTI, "cikis_turu", "cesit", dgw.Rows[row].Cells["cesit"].Value.ToString(), "", ""));
            komut.Parameters.AddWithValue("@_cikis_tutar", Convert.ToDecimal(dgw.Rows[row].Cells["miktar"].Value.ToString()));

            if (bankaAdi != "")
            {
                komut.Parameters.AddWithValue("@_banka_id", sqlIslemler.idGetir(constants.LOCAL_BAGLANTI, "bankalar", "banka_adi", dgw.Rows[row].Cells["bankaAdi"].Value.ToString(), "", ""));
                komut.Parameters.AddWithValue("@_banka_islem_tarih", dgw.Rows[row].Cells["Tarih"].Value.ToString());
            }
            else
            {
                komut.Parameters.AddWithValue("@_banka_id", (0));
                komut.Parameters.AddWithValue("@_banka_islem_tarih", DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"));
            }


            komut.Parameters.AddWithValue("@_tarih", DateTime.Now.ToString("yyyy-MM-dd"));
            komut.Parameters.AddWithValue("@_tarih_zaman", DateTime.Parse(dgw.Rows[row].Cells["tarih"].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            komut.Parameters.AddWithValue("@_islem_yapan", dgw.Rows[row].Cells["islemiYapan"].Value.ToString());
            komut.Parameters.AddWithValue("@_islem_not", dgw.Rows[row].Cells["islemNot"].Value.ToString());

            komut.Parameters.AddWithValue("@_web_kayit", webKayit);

            try
            {

                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }//

        public bool urunStokYereldeVeriKaydet(DataGridView dgw, int row, int firmaWebId, bool onay, bool webKayit, string baglanti)
        {
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into urun_stok (firma_web_id, urun_adi, urun_fiyat, stok_adedi, urun_barkod, talep_tarih, onay, onay_tarih, web_kayit) " +

                   "values (@_firma_web_id, @_urun_adi, @_urun_fiyat, @_stok_adedi, @_urun_barkod, @_talep_tarih, @_onay, @_onay_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into urun_stok (firma_web_id, urun_adi, urun_fiyat, stok_adedi, urun_barkod, talep_tarih, onay, onay_tarih, web_kayit) " +

                   "values (@_firma_web_id, @_urun_adi, @_urun_fiyat, @_stok_adedi, @_urun_barkod, @_talep_tarih, @_onay, @_onay_tarih, @_web_kayit)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }


            komut.Parameters.AddWithValue("@_firma_web_id", firmaWebId);
            komut.Parameters.AddWithValue("@_urun_adi", dgw.Rows[row].Cells["AD"].Value.ToString());
            komut.Parameters.AddWithValue("@_urun_fiyat", Convert.ToDecimal(dgw.Rows[row].Cells["Fiyat"].Value.ToString()));
            komut.Parameters.AddWithValue("@_stok_adedi", Convert.ToInt32(dgw.Rows[row].Cells["Stok Adedi"].Value.ToString()));
            komut.Parameters.AddWithValue("@_urun_barkod", dgw.Rows[row].Cells["Bar Kod"].Value.ToString());

            komut.Parameters.AddWithValue("@_talep_tarih", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            komut.Parameters.AddWithValue("@_onay", onay);
            komut.Parameters.AddWithValue("@_onay_tarih", DateTime.MinValue.ToString("yyyy-MM-dd"));
            komut.Parameters.AddWithValue("@_web_kayit", webKayit);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }///

        public bool urunStokTalepKontrol(string urun, string baglanti)
        {
            DataTable dtLocal = sqlIslemler.butunVerileriSiraliGetir("urun_stok", constants.LOCAL_BAGLANTI);

            for (int i = 0; i < dtLocal.Rows.Count; i++)
            {
                if (dtLocal.Rows[i]["urun_adi"].ToString().Equals(urun))
                {
                    if (dtLocal.Rows[i]["onay"].ToString().Equals(constants.TRUE_STR) ||
                        dtLocal.Rows[i]["onay"].ToString().Equals(bool.TrueString)) return true;
                }

            }

            return false;
        }///

        public string urunStokUrunKontrol(DataGridView dgw, string baglanti)
        {
            DataTable dtLocal = sqlIslemler.butunVerileriSiraliGetir("urun_stok", constants.LOCAL_BAGLANTI);
            string ayniAd = "";
            string s = null;

            for (int i = 0; i < dgw.Rows.Count - 1; i++)
            {
                for (int sutun = 0; sutun < dgw.Columns.Count; sutun++)
                {
                    if (dgw.Rows[i].Cells[sutun].Value == null || dgw.Rows[i].Cells[sutun].Value.ToString() == "")
                    {
                        s = "Eksik giriş yapılmış ! Ürün bilgelirini tamamlayınız...";
                        return s;
                    }

                }
                ayniAd = dgw.Rows[i].Cells["AD"].Value.ToString();

                for (int j = i + 1; j < dgw.Rows.Count; j++)
                {
                    if (dgw.Rows[j].Cells["AD"].Value != null)
                    {
                        if (ayniAd.Equals(dgw.Rows[j].Cells["AD"].Value.ToString()))
                        {
                            s = "' " + ayniAd + " '" + " isimde birden fazla giriş yapılmış.";
                            return s;
                        }
                    }

                }
            }

            for (int i = 0; i < dtLocal.Rows.Count; i++)
            {
                for (int j = 0; j < dgw.Rows.Count - 1; j++)
                {
                    if (dtLocal.Rows[i]["urun_adi"].ToString().Equals(dgw.Rows[j].Cells["AD"].Value.ToString()))
                    {
                        for (int x = 0; x < dgw.Columns.Count; x++)
                        {
                            s += dgw.Rows[j].Cells[x].Value.ToString() + ",";
                        }
                        return s;
                    }
                }
            }

            return s;

        }///

        public bool veriKartAyniTarihSil(string oyuncak_id, string tarih, string baglanti)
        {
            sorgu = "Delete From oyuncak_takip Where firma_web_id=@_firma_web_id and oyuncak_id=@_oyuncak_id and tarih=@_tarih";

            if (baglanti == constants.LOCAL_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            if (baglanti == constants.UZAK_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(sabitIslem.firmaWebIdGetir()));
            komut.Parameters.AddWithValue("@_oyuncak_id", oyuncak_id);
            komut.Parameters.AddWithValue("@_tarih", DateTime.Parse(tarih));


            try
            {
                if (baglanti == constants.LOCAL_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                }
                if (baglanti == constants.UZAK_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                }


                return true;

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

        }//

        public bool oyuncak_ayar_oyuncak_sil(string oyuncakID, string baglanti)
        {
            sorgu = "Delete From oyuncak_ayar Where firma_web_id=@_firma_web_id and oyuncak_id=@_oyuncak_id";


            if (baglanti == constants.LOCAL_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            if (baglanti == constants.UZAK_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(sabitIslem.firmaWebIdGetir()));
            komut.Parameters.AddWithValue("@_oyuncak_id", oyuncakID);

            try
            {
                if (baglanti == constants.LOCAL_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                }
                if (baglanti == constants.UZAK_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                }


                return true;

            }

            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;
        }

        public bool oyuncakKayitSil(string oyuncakID, string baglanti)
        {
            sorgu = "Delete From oyuncak_tanimla Where firma_web_id=@_firma_web_id and oyuncak_id=@_oyuncak_id";


            if (baglanti == constants.LOCAL_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);
            if (baglanti == constants.UZAK_BAGLANTI)
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(sabitIslem.firmaWebIdGetir()));
            komut.Parameters.AddWithValue("@_oyuncak_id", oyuncakID);

            try
            {
                if (baglanti == constants.LOCAL_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                }
                if (baglanti == constants.UZAK_BAGLANTI)
                {
                    sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                }


                return true;

            }

            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }

            return false;
        }//

        public bool oyuncakKayitGuncelle(string oyuncakID, string baglanti, string[] yeniVeriler)
        {

            return false;
        }
        //

        public bool telafiFormVeriKaydet(string telafi_islem, string mevcut_tl, string yuk_tl, string tag_id, string oyuncak,
                                         string aciklama, string kasiyer, string baglanti, bool web_kayit, string telafiTalepKod)
        {
            string firma_web_id = sabitIslem.firmaWebIdGetir();
            string tarihZamanSimdi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "Insert into telafi_takip(firma_web_id, telafi_islem, kart_mevcut_tl, yuklenen_telafi_tl,tag_id, oyuncak, aciklama, kasiyer, tarih, web_kayit, evrak_no_kod) " +

                   "values (@_firma_web_id, @_telafi_islem, @_kart_mevcut_tl,@_yuklenen_telafi_tl, @_tag_id, @_oyuncak, @_aciklama, @_kasiyer, @_tarih, @_web_kayit, @_evrak_no_kod)";

                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "Insert into telafi_takip(firma_web_id, telafi_islem, kart_mevcut_tl, yuklenen_telafi_tl,tag_id, oyuncak, aciklama, kasiyer, tarih, web_kayit, evrak_no_kod) " +

                   "values (@_firma_web_id, @_telafi_islem, @_kart_mevcut_tl,@_yuklenen_telafi_tl, @_tag_id, @_oyuncak, @_aciklama, @_kasiyer, @_tarih, @_web_kayit, @_evrak_no_kod)";

                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);
            }

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(firma_web_id));
            komut.Parameters.AddWithValue("@_telafi_islem", telafi_islem);
            komut.Parameters.AddWithValue("@_kart_mevcut_tl", Convert.ToDecimal(mevcut_tl));
            komut.Parameters.AddWithValue("@_yuklenen_telafi_tl", Convert.ToDecimal(yuk_tl));
            komut.Parameters.AddWithValue("@_tag_id", tag_id);
            komut.Parameters.AddWithValue("@_oyuncak", oyuncak);
            komut.Parameters.AddWithValue("@_aciklama", aciklama);
            komut.Parameters.AddWithValue("@_kasiyer", kasiyer);
            komut.Parameters.AddWithValue("@_tarih", DateTime.Parse(tarihZamanSimdi));
            komut.Parameters.AddWithValue("@_web_kayit", web_kayit);
            if (telafiTalepKod != "")
                komut.Parameters.AddWithValue("@_evrak_no_kod", telafiTalepKod);
            else komut.Parameters.AddWithValue("@_evrak_no_kod", null);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }//

        public bool webTelafiTalepOnayVeriGuncelle(string talepVeri)
        {
            string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");

            sorgu = "UPDATE buf_telafi_onay Set evrak_no_kod=@_evrak_no_kod Where firma_web_id='" + firmaWebId + "'";
            komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            komut.Parameters.AddWithValue("@_evrak_no_kod", talepVeri);
            try
            {
                if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                {
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }

        public void firmaCekilisTakipWebKayitGuncelle(DataTable dt)
        {
            string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");
            foreach (DataRow row in dt.Rows)
            {
                if (row["firma_web_id"].ToString() == firmaWebId)
                    web_kayit_durum_guncelle("firma_cekilis_takip", int.Parse(row["id"].ToString()), true, constants.UZAK_BAGLANTI);
            }
        }

        public bool firmaCekilisKullanimGuncelle(string cekilisKodu, string adSoyad, string baglanti)
        {
            // string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                sorgu = "UPDATE firma_cekilis_takip Set kullanim=@_kullanim Where cekilis_no='" + cekilisKodu + "' and ad_soyad='" + adSoyad + "'";
                komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            }
            if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                sorgu = "UPDATE firma_cekilis_takip Set kullanim=@_kullanim Where cekilis_no='" + cekilisKodu + "' and ad_soyad='" + adSoyad + "'";
                komut = new MySqlCommand(sorgu, sqlIslemler.uzakBaglanti);

            }

            komut.Parameters.AddWithValue("@_kullanim", true);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
                    return true;
                }

                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    if (sqlIslemler.baglantiAc(sqlIslemler.uzakBaglanti))
                    {
                        komut.ExecuteNonQuery();
                        sqlIslemler.baglantiKapat(sqlIslemler.uzakBaglanti);
                        return true;
                    }
                    else return false;

                }

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;
        }

        public void firmaCekilisTakipVeriKaydet(DataTable dt)
        {
            string[] veriler = new string[dt.Columns.Count];
            string firmaWebId = sqlIslemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");

            DateTime tarih = DateTime.Now;

            foreach (DataRow row in dt.Rows)
            {
                DateTime bitis = DateTime.Parse(row["bit_tarih"].ToString());

                if (bitis >= tarih && firmaWebId == row["firma_web_id"].ToString())
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        veriler[i] = row[i].ToString();
                    }

                    firmaCekilisTakipSatirekle(veriler);
                }
            }
        }

        public bool firmaCekilisTakipSatirekle(string[] veriler)
        {
            sorgu = "Insert into firma_cekilis_takip(firma_web_id, ad_soyad, cekilis_no, bas_tarih,	bit_tarih, cekilis_miktari, kullanim, web_kayit) " +

                   "values (@_firma_web_id, @_ad_soyad, @_cekilis_no, @_bas_tarih, @_bit_tarih, @_cekilis_miktari, @_kullanim, @_web_kayit)";

            komut = new MySqlCommand(sorgu, sqlIslemler.localBaglanti);

            komut.Parameters.AddWithValue("@_firma_web_id", Convert.ToInt32(veriler[1]));
            komut.Parameters.AddWithValue("@_ad_soyad", veriler[2]);
            komut.Parameters.AddWithValue("@_cekilis_no", Convert.ToDecimal(veriler[3]));
            komut.Parameters.AddWithValue("@_bas_tarih", DateTime.Parse(veriler[4]));
            komut.Parameters.AddWithValue("@_bit_tarih", DateTime.Parse(veriler[5]));
            komut.Parameters.AddWithValue("@_cekilis_miktari", Convert.ToDecimal(veriler[6]));

            if (veriler[7] == Boolean.TrueString)
                komut.Parameters.AddWithValue("@_kullanim", true);
            else
                komut.Parameters.AddWithValue("@_kullanim", false);

            if (veriler[8] == Boolean.FalseString)
                komut.Parameters.AddWithValue("@_web_kayit", true);

            else
                komut.Parameters.AddWithValue("@_web_kayit", true);

            try
            {
                sqlIslemler.baglantiAc(sqlIslemler.localBaglanti);
                komut.ExecuteNonQuery();
                sqlIslemler.baglantiKapat(sqlIslemler.localBaglanti);
            }
            catch (Exception ex)
            {
                mBox.Show("HATA:" + ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
            return false;

        }

    }//class tabloVeriEkle
}

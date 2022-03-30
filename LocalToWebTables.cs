using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing.Printing;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace socketio_client
{
    class LocalToWebTables
    {
        mySql_islemler MySqlOperations = new mySql_islemler();
        DataTable dt = new DataTable();

        mbox mBox = new mbox();
        constants ConstOperations = new constants();

        tabloVeriEkle TableAddData = new tabloVeriEkle();
        MySqlCommand mysqlCmd = new MySqlCommand();

        string query = "";
        string[] dataArr;

        public string getWebLastDateTime(string companyID, string tableName)
        {
            if (tableName == "satislar" || tableName == "misafir_odemeler" || tableName == "kasa_cikis")
                query = "SELECT MAX(tarih_zaman) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "misafirler")
                query = "SELECT MAX(misafir_bas_zaman) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "abone_takip" || tableName== "gecici_kart_takip" || tableName== "gunluk_kasa" || tableName== "personel_takip" ||
                 tableName== "telafi_takip")
                query = "SELECT MAX(tarih) FROM "+tableName+" WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "personel_listesi")
                query = "SELECT MAX(ise_baslama_tar) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "kullanicilar" )
                query = "SELECT MAX(son_giris_tarihi) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "kullanici_giris_takip")
                query = "SELECT MAX(basarili_girisler) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            if (tableName == "iptal_takip")
                query = "SELECT MAX(tarih_saat) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";       

            if (tableName == "urun_stok")
                query = "SELECT MAX(talep_tarih) FROM " + tableName + " WHERE firma_web_id = '" + companyID + "'";

            mysqlCmd = new MySqlCommand(query, MySqlOperations.uzakBaglanti);
            try
            {
                if(MySqlOperations.baglantiAc(MySqlOperations.uzakBaglanti))
                {
                    var obj = mysqlCmd.ExecuteScalar();

                    if (obj != DBNull.Value)
                    {
                        MySqlOperations.baglantiKapat(MySqlOperations.uzakBaglanti);

                        return DateTime.Parse(obj.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    
                    MySqlOperations.baglantiKapat(MySqlOperations.uzakBaglanti);
                    return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public DataTable getTableLastDateTimeData(string tableName, string dateTime)
        {
            if (tableName == "satislar" || tableName == "misafir_odemeler" || tableName == "kasa_cikis")
                query = "SELECT *FROM " + tableName + " WHERE tarih_zaman>'" + dateTime + "'";

            if (tableName == "misafirler")
                query = "SELECT *FROM " + tableName + " WHERE misafir_bas_zaman>'" + dateTime + "'";

            if (tableName == "abone_takip" || tableName == "gecici_kart_takip" || tableName == "gunluk_kasa" || tableName == "personel_takip" ||
                 tableName == "telafi_takip")
                query = "SELECT *FROM " + tableName + " WHERE tarih>'" + dateTime + "'";

            if (tableName == "personel_listesi")
                query = "SELECT *FROM " + tableName + " WHERE ise_baslama_tar >'" + dateTime + "'";

            if (tableName == "kullanicilar")
                query = "SELECT *FROM " + tableName + " WHERE son_giris_tarihi >'" + dateTime + "'";

            if (tableName == "kullanici_giris_takip")
                query = "SELECT *FROM " + tableName + " WHERE basarili_girisler >'" + dateTime + "'";

            if (tableName == "iptal_takip")
                query = "SELECT *FROM " + tableName + " WHERE tarih_saat >'" + dateTime + "'";

            if (tableName == "urun_stok")
                query = "SELECT *FROM " + tableName + " WHERE talep_tarih >'" + dateTime + "'";

            dt = MySqlOperations.dataTableGetir(query, MySqlOperations.localBaglanti);

            if (dt.Rows.Count > 0) return dt;
            else return null;
        }

        public bool updateWebKayitToTrue(string tableName)
        {
           // if (tableName == "abone_takip")
           query = "UPDATE " + tableName + " SET web_kayit='1' WHERE web_kayit='0'";

            mysqlCmd = new MySqlCommand(query, MySqlOperations.localBaglanti);

            try
            {
                MySqlOperations.baglantiAc(MySqlOperations.localBaglanti);
                mysqlCmd.ExecuteNonQuery();
                MySqlOperations.baglantiKapat(MySqlOperations.localBaglanti);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;

        }

        public string makeInsertQueryTable(string tableName, string queryColName)
        {
            string latestDateTime = getWebLastDateTime(MySqlOperations.firmaWebIdGetir(), tableName);
            DataTable dt = getTableLastDateTimeData(tableName, latestDateTime);
            if (dt == null) return null;

            string queryValues = "";

            foreach (DataRow row in dt.Rows)
            {
                string values = "(";
                string rowColValue = "";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (row[i] == DBNull.Value || row[i].ToString() == "")
                        rowColValue = "null";
                    else
                        rowColValue = row[i].ToString();

                    if (IsDateTime(rowColValue))
                        rowColValue = DateTime.Parse(rowColValue).ToString("yyyy-MM-dd HH:mm:ss");

                    if (rowColValue == "False")
                        rowColValue = "1";
                    if (rowColValue == "True")
                        rowColValue = "1";

                    if (rowColValue.Contains(","))
                    {
                        rowColValue = rowColValue.Replace(",", ".");
                    }

                    rowColValue = "'" + rowColValue + "'";

                    if (values == "(") values += rowColValue;
                    else values += ", " + rowColValue;
                }
                values += ")";

                if (queryValues == "") queryValues += values;
                else queryValues += "," + values;
            }

            return queryColName + queryValues;
        }//

        public const int NO_NEW_VALUE= 2;
        public int setTableDataToWeb(string tableName)
        {
            string queryInsertColName = "";

            if (tableName == "abone_takip")
            {
                queryInsertColName = "INSERT INTO abone_takip " +
                                     "(firma_web_id, ad_soyad, veli_ad, tel, abone_tur, abone_bas_bit_tarih, tag_id, kontur_tl, durum, tarih, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
                // query = makeQueryAboneTakipTable();
            }

            if (tableName == "satislar")
            {
                queryInsertColName = "INSERT INTO satislar (firma_web_id, urun_id, urun_adet, toplam_tutar, odeme_turu, tarih, tarih_zaman, " +
                                     "web_kayit,satis_tur,tag_id, aciklama, kupon_seri_no) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "misafirler")
            {
                queryInsertColName = "INSERT INTO misafirler (firma_web_id, misafir_adi, misafir_veli, misafir_veli_tel, misafir_sure, misafir_ek_sure, " +
                                     "misafir_tarih, misafir_bas_zaman, misafir_bit_zaman, ayakkabilik_no, izin_adi, misafir_aciklama, misafir_durum, "+
                                     "web_kayit, izin_bitis_suresi, soft_alan, sms_sayisi) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "misafir_odemeler")
            {
                queryInsertColName = "INSERT INTO misafir_odemeler (firma_web_id, misafir_id, grup_kardes_id, tutar, indirim_tur, tarih, tarih_zaman, "+
                                     "odeme_turu, web_kayit, kupon_seri_no) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "kasa_cikis")
            {
                queryInsertColName = "INSERT INTO kasa_cikis (firma_web_id, cikis_adi, cikis_tur_id, cikis_tutar, banka_id, banka_islem_tarih, " +
                                       "tarih, tarih_zaman, islem_yapan, islem_not, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "gecici_kart_takip")
            {
                queryInsertColName = "INSERT INTO gecici_kart_takip (web_id, firma_web_id, kasiyer, personel, kart_id, kart_tip, yuklenen_miktar, " +
                                    "kalan_miktar, kart_yer_bilgisi, tarih, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "gunluk_kasa")
            {
                queryInsertColName = "INSERT INTO gunluk_kasa (firma_web_id, toplam_satis, toplam_islem, toplam_ciro, nakit_para_tutar, kredi_karti_tutar, banka_tutar, " +
                                    "masraf_tutar, gunluk_kasa, devreden_miktar, fark, tarih, kasiyer_devreden, kasiyer_devralan, varsa_not, islem_tur, web_kayit, "+
                                    "picc_stok, z_raporu_toplam, z_raporu_kk) VALUES "; 
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "personel_takip")
            {
                queryInsertColName = "INSERT INTO personel_takip (firma_web_id, ad_soyad, calistigi_yer, kart_id, tarih, giris_cikis, duzenleme, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "personel_listesi")
            {
                queryInsertColName = "INSERT INTO personel_listesi (web_id, firma_web_id, ad_soyad, cinsiyet, calistigi_yer, ise_baslama_tar, isten_ayrilma_tar, "+
                                    "tel, adres, kart_id, is_durum, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName.Contains("kullanicilar-delete"))
            {
                query ="DELETE FROM `kullanicilar` WHERE firma_web_id = '"+tableName.Split('-')[2]+"'";
                 
            }

            if (tableName == "kullanicilar")
            {
                queryInsertColName = "INSERT INTO kullanicilar (firma_web_id, kullanici_adi, kullanici_sifre, kullanici_yetki, sifre_degisim_sayisi, "+
                                     "son_giris_tarihi, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "kullanici_giris_takip")
            {
                queryInsertColName = "INSERT INTO kullanici_giris_takip (firma_web_id, kullanici_id, basarili_girisler, basarisiz_girisler, web_kayit)" +
                                     " VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "iptal_takip")
            {
                queryInsertColName = "INSERT INTO iptal_takip (firma_web_id, tarih_saat, iptal_eden, islem_turu, aciklama, web_kayit) VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName == "urun_stok")
            {
                queryInsertColName = "INSERT INTO urun_stok (firma_web_id, urun_adi, urun_fiyat, stok_adedi, urun_barkod, talep_tarih, onay, onay_tarih, web_kayit) " +
                                       " VALUES ";
                query = makeInsertQueryTable(tableName, queryInsertColName);
            }

            if (tableName.Contains("urun-delete"))
            {
                query = "DELETE FROM urun_stok WHERE firma_web_id = '" + tableName.Split('-')[2] + "'";

            }

            if (query == null) return NO_NEW_VALUE;

            mysqlCmd = new MySqlCommand(query, MySqlOperations.uzakBaglanti);
            try
            {
                if (MySqlOperations.baglantiAc(MySqlOperations.uzakBaglanti))
                {
                    mysqlCmd.ExecuteNonQuery();

                    MySqlOperations.baglantiKapat(MySqlOperations.uzakBaglanti);

                    updateWebKayitToTrue(tableName);

                    return 1;
                }
                else return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }

        public bool updateGeciciKartWeb(string tagId, string value)
        {
            query = "UPDATE gecici_kart_takip SET kalan_miktar = '"+ value +"' WHERE kart_id = '"+tagId+"'";
            mysqlCmd = new MySqlCommand(query, MySqlOperations.uzakBaglanti);

            try
            {
                if(MySqlOperations.baglantiAc(MySqlOperations.uzakBaglanti))
                {
                    mysqlCmd.ExecuteNonQuery();
                    MySqlOperations.baglantiKapat(MySqlOperations.localBaglanti);
                    return true;
                }
                else
                {
                    MySqlOperations.baglantiKapat(MySqlOperations.localBaglanti);
                    return false;
                }              
            }
            catch (Exception e)
            {
                MySqlOperations.baglantiKapat(MySqlOperations.localBaglanti);
                return false;
            }
            return false;

        }

        public bool delduplicateRowInAboneTakip(string connection)
        {
            query = "DELETE FROM  abone_takip WHERE  id NOT IN(SELECT * FROM(SELECT MIN(n.id) FROM abone_takip n GROUP BY n.tag_id) X)";

            if (connection == constants.LOCAL_BAGLANTI)
                mysqlCmd = new MySqlCommand(query, MySqlOperations.localBaglanti);
            if (connection == constants.UZAK_BAGLANTI)
                mysqlCmd = new MySqlCommand(query, MySqlOperations.uzakBaglanti);

            try
            {
                if (connection == constants.LOCAL_BAGLANTI)
                {
                    MySqlOperations.baglantiAc(MySqlOperations.localBaglanti);
                    mysqlCmd.ExecuteNonQuery();
                    MySqlOperations.baglantiKapat(MySqlOperations.localBaglanti);
                    return true;
                }
                if (connection == constants.UZAK_BAGLANTI)
                {
                    MySqlOperations.baglantiAc(MySqlOperations.uzakBaglanti);
                    mysqlCmd.ExecuteNonQuery();
                    MySqlOperations.baglantiKapat(MySqlOperations.uzakBaglanti);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public bool IsDateTime(string date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            return DateTime.TryParse(date, out DateTime dateTime);
        }

        public string makeQueryAboneTakipTable()
        {
            string latestDateTime = getWebLastDateTime(MySqlOperations.firmaWebIdGetir(), "abone_takip");
            DataTable dt = getTableLastDateTimeData("abone_takip", latestDateTime);           

            if (dt == null) return null;  //there isn't new value on the table.

            string queryInsert = "INSERT INTO abone_takip " +
                                 "(firma_web_id, ad_soyad, veli_ad, tel, abone_tur, abone_bas_bit_tarih, tag_id, kontur_tl, durum, tarih, web_kayit) VALUES ";
            string queryValues = "";  

            foreach (DataRow row in dt.Rows)
            {
                string values = "(";
                string rowColValue = "";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (row[i] == DBNull.Value || row[i].ToString()=="")
                        rowColValue = "null";
                    else
                        rowColValue = row[i].ToString();

                    if (IsDateTime(rowColValue))
                        rowColValue = DateTime.Parse(rowColValue).ToString("yyyy-MM-dd HH:mm:ss");

                    if (rowColValue == "False")
                        rowColValue = "1";
                    if (rowColValue == "True")
                        rowColValue = "1";

                    if (rowColValue.Contains(","))
                    {
                        rowColValue = rowColValue.Replace(",", ".");
                    }

                    rowColValue = "'" + rowColValue + "'";

                    if (values=="(") values += rowColValue;
                    else values +=", "+ rowColValue;
                }
                values += ")";

                if (queryValues == "") queryValues += values;
                else queryValues += "," + values;
            }
            return queryInsert+queryValues;
        }   
     

     


    }//class
}//namespace

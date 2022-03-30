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
    class constants
    {
        public const int ISLEM_GIRIS = 1;
        public const int ISLEM_KASA_DEVRET = 2;
        public const int ISLEM_GUN_SONU = 3;
        public const int ISLEM_AYARLAR =4 ;
        public const int ISLEM_SIFRE_DEGISIM_SAYISI_SIL = 5;
        public const int ISLEM_RAPORLAR = 6;
        public const int ISLEM_PERSONEL= 7;
        public const int ANLIK = 256;

        public const int FIRMA_ILK_KAYIT_ID = 0;
        public const int FIRMA_ORTAKLIK_DEFAULT= 1;

        public const int ADMIN_ID = 1;

        public const int ETIKET_YAZDIR = 11;
        public const int ETIKET_YAZDIRMA = 10;
        public const int GUNSONU_YAZDIR = 21;
        public const int GUNSONU_YAZDIRMA = 20;
        public const int KASADEVIR_YAZDIR = 31;
        public const int KASADEVIR_YAZDIRMA = 30;
        public const int TABLO_OTO_SIRALA = 41;
        public const int TABLO_OTO_SIRALAMA = 40;

        public const int KULLANICI_ISMI_GUNCELLE = 200;
        public const int SURE_BUTON_ISMI_GUNCELLE = 201;
        public const int MIS_ADSOYAD_GUNCELLE = 202;
        public const int TEXTBOX_LABEL_GUNCELLE= 203;
        public const int BUTTON_EN_GUNCELLE= 204;
        public const int ADMIN_YETKI_GIRISI=205;
        public const int PROG_ANA_SATIS = 206;
        public const int PROG_KULLANICI_TAKIP = 207;
 
        public const int TIP_INT = 101;
        public const int TIP_STR = 100;
        public const int TIP_DATETIME = 102;
        public const int TIP_BOOL = 103;
        public const int TIP_DECIMAL = 104;

        public const int SURE_FIYAT_WEB_ =0;
        public const int SURE_FIYAT_ILK_KAYIT = 1;
        public const int SURE_FIYAT_YENI_TALEP = 2;
        public const bool SURE_FIYAT_EKLEME_ISLEMI = true;
        public const bool SURE_FIYAT_GUNCELLEME_ISLEMI = false;
        public const int SURESIZ_VADELI_FIYAT =0;

        public const string USER_YETKI = "2";
        public const string ADMIN_YETKI= "1";

        public const string SURESI_BITMIS = "-1";// Misafir havuzdan ayrıldığında suresi bitmiş durumunda olur.
        public const string SURESI_BASLADI = "+1";
        public const string HAVUZ_DISI = "-1";
        public const string ACIKLAMA_HAVUZ_DISI = "HAVUZ DIŞI";

       
        public const string ISLEM_EK_SURE = "EK SURELI";
        public const string EKSURE_GECIS = "EKSURE_GECIS";
        public const string EKSURE_EKLE = "EKSURE_EKLE";
        public const string ISLEM_SURELI= "SURELI";
        public const string ISLEM_SURESIZ = "SURESIZ";
        public const string ISLEM_SURESIZ_PESIN = "SURESIZ PESIN";
        public const string ISLEM_SURESIZ_VADELI = "SURESIZ VADELI";
        public const string ISLEM_GUNLUK_LISTE = "GUNLUK LISTE";
        public const string ISLEM_SURESI_BITENLER_LISTE = "-1";
        public const string ISLEM_SURESI_BASLADI_LISTE = "+1";
        public const string ISLEM_HAVUZ_DISI_LISTE = "HAVUZ DISI";
        public const string ISLEM_5DK_ALTI = "5DK";
        public const string ISLEM_ODEME_SURESIZ = "ODEME SURESIZ";
        public const string ISLEM_ODEME_SURESIZ_PESIN = "ODEME SURESIZ PESIN";
        public const string ISLEM_ODEME_NEGATIF_SURE = "NEGATIF SURE";
        public const string ISLEM_IPTAL = "IPTAL";
        public const string PICC_ISLEM_IPTAL = "IPTAL-TEMASSIZ KART";
        public const string ISLEM_IPTAL_ODEME = "IPTAL ODEMESİ";
        public const string ISLEM_MIS_TABLO_TUMU = "MIS TABLO TUMU";

        public const string ISLEM_PROM_EK60DK = "PROM EK60DK";


        public const string ISLEM_IZIN_WC = "WC";
        public const string ISLEM_IZIN_YEMEK= "YEMEK";
        public const string ISLEM_IZIN_DIGER = "DIGER";
        public const string ISLEM_IZIN_KALDIR = "...";

        public const string LOCAL_BAGLANTI = "YEREL";
        public const string UZAK_BAGLANTI = "UZAK";

        public const string ODEME_TUR_NAKIT = "NAKIT";
        public const string ODEME_TUR_KKARTI = "KKARTI";
        public const string ODEME_TUR_KUPON = "KUPON";
        public const string ODEME_TUR_PICC = "TEMASSIZ KART";
        public const string ODEME_TUR_TELAFI = "TELAFI KONTUR";
        public const string ODEME_TUR_KART_PROMASYON = "KART PROMASYON"; // karta yüklenen promasyon miktarını belirtir. 
        public const string ODEME_TUR_KART_DEPOSITO = "KART_DEPOSITO";
        public const string ODEME_TUR_KART_DEPOSITO_KKARTI = "KART_DEPOSITO_KKARTI";
        public const string ODEME_TUR_KART_DEPOSITO_NAKIT= "KART_DEPOSITO_NAKIT";
        public const string ODEME_TUR_KART_DEPOSITO_IADE = "KART_DEPOSIT_IADE";
        public const string KART_STOK_ADI = "TEMASSIZ KART";

        public const string KASA_CIKIS_TUR_MASRAF = "MASRAF";
        public const string KASA_CIKIS_TUR_ODEME = "ODEME";
        public const string KASA_CIKIS_TUR_BANKA = "BANKA";
        public const string KASA_CIKIS_TUR_ELDEN = "ELDEN VERILEN";

        public const string TRUE_STR = "1";
        public const string FALSE_STR ="0";

        public const string PROGRAM_ILKKURULUM_LOCAL = "ILK KURULUM LOCAL";
        public const string PROGRAM_ILKKURULUM_WEB = "ILK KURULUM WEB";
        public const string PROGRAM_DEMO_LOCAL = "DEMO LOCAL";                   //2 hafta yada 1 ay sonra göçmeli
        public const string PROGRAM_DEMO_WEB = "DEMO WEB"; 
        public const string PROGRAM_KAYITLI_LOCAL = "KAYITLI LOCAL";
        public const string PROGRAM_KAYITLI_WEB = "KAYITLI WEB";

        public const string INDIRIM_BUTUN_SURELER = "HEPSI";

        public const int INTERNET_YOK = 0;
        public const int UZAK_BAGLANTI_YOK = 1;
        public const int UZAK_BAGLANTI_VAR = 2;
        public const int UZAK_BAGLANTI_ACIK = 3;
        public const int PING_HOST = 4;

        public const int ONAYLANMAMIS_SURE_FIYAT = 0;
        public const int ONAYLI_SURE_FIYAT = 1;
        public const int ONAYLANMIS_SURE_FIYAT = 2;

        //ana bilgisayar : Birden fazla bilgisayarın olduğu yerlerde, işletmenin muhasabesinin takip edileceği bilgisayar.
        // Temassız karta kontur yükleme noktası. Burada bütün oyuncaklar ve oyun alanları kart ile çalışır...
        // Oyuncaklara jeton karta yüklenen TL para miktarından düşülerek girilir..
        // Ödeme işlemi karta yüklenen TL para miktarından düşülerek yapılır...
        // Oyun alanlarındaki diğer bilgisayarlardaki programlar ise sadece misafir takipi için kullanılır. 
        // "prog_rol_is" tablosunda bu durum kayıtlıdır...  

        public const string PROG_ROL_ANA_MAKINE = "ANA";        // ana makine,  satış ve takip yapabilir. 
                                                                //Kullanıcılar ana makine üzerinden işletmenin genel muhasebesine dahil olurlar.
        public const string PROG_ROL_KULLANICI_MAKINE = "KULLANICI";  // kullanıcı, satış ve takip yapabilir. hem kendi veri tabanına hem de
                                                                //gerekli olan bilgileri ana makine veri tabanına kaydeder..

        public const string PROG_IS_SATIS = "SATIS";
        public const string PROG_IS_SATIS_TAKIP = "SATIS-TAKIP";
        public const string PROG_IS_TAKIP = "TAKIP";
        public const string PROG_IS_TYOK= "YOK";


        public const string SATIS_TUR_URUN= "URUN";                      //satislar tablosunda satis_tur sutununda  urun satışını ifade eder.
        public const string SATIS_TUR_KART_YUKLEME = "KART YUKLEME";     // satislar tablosunda satis_tur sutununda  karta yükleme yapıldığını ifade eder.
        public const string SATIS_TUR_KART_YUKLEME_GECICI = "KART YUKLEME GECICI";
        public const string SATIS_TUR_KART_SURELI_SINIRSIZ_ABONE = "KART SINIRSIZ ABONE";
        public const int SATIS_TUR_JETON_TL_ID=0;
        public const string SATIS_CIRO = "CIRO";

        public const string TABLO_MISAFIRLER = "misafirler";
        public const string TABLO_SATISLAR = "satislar";
        public const string TABLO_MISAFIR_ODEMELER = "misafir_odemeler";
        public const string TABLO_SURE_ISIM_FIYAT_KUM_HAVUZU = "sure_buton_isim_fiyatlandirma";
        public const string TABLO_SURE_ISIM_FIYAT_TOP_HAVUZU = "sure_buton_isim_fiyatlandirma2";

        public const int SOFT_ALAN_KUM_HAVUZU = 1;
        public const int SOFT_ALAN_TOP_HAVUZU = 0;

        public const string KUM_HAVUZU = "Kum HAVUZU";
        public const string TOP_HAVUZU = "Top HAVUZU";

        public const int USB_OUT_VERI_TIP = 1;
        public const int USB_OUT_VERI_BOS = 2;
        public const int USB_OUT_VERI_TAGID = 3;
        public const int USB_OUT_TAM_KISIM = 8;     //buffer low-high şeklinde yerleşiyor
        public const int USB_OUT_TAM_KISIM_HIGH = 9;    
        public const int USB_OUT_ONDALIK_KISIM = 10;

        public const char NRF_OYUN_BASLAT = 'B';
        public const char NRF_OYUN_SONLANDIR='S';
        

        public const char USB_OUT_TIP_TAG_KONTUR_YAZ = 'W';
        public const char USB_OUT_TIP_TAG_KONTUR_OKU = 'R';
        public const char USB_OUT_TIP_TAG_KONTUR_SIL = 'S';
        public const char USB_OUT_TIP_TAG_KART_IADE = 'Z';
        public const char USB_OUT_TIP_TAG_TELAFI_YAZ = 'T';
        public const char USB_OUT_TIP_SIFRE_GONDER = 'p';
        public const char USB_OUT_TIP_PROG_DURUM_GONDER = 'g';
        public const char USB_OUT_TIP_PROM_GONDER = 'm';
        public const char USB_OUT_TIP_DEPOSIT_GONDER = 'd';
        public const char USB_OUT_TIP_KART_IADE_GONDER = 'q';
        public const char USB_OUT_TIP_KART_DURUM_OKU_GONDER = 'u';
        public const char USB_OUT_TIP_NRF_VERI_GONDER = 'n';
        public const char USB_OUT_TIP_NRF_OID_GONDER = 'i';
        public const char USB_OUT_TIP_NRF_RST_ID_GONDER = '?';
        public const char USB_OUT_TIP_NRF_PING_GONDER = 'L';
        public const char USB_OUT_TIP_NRF_ACK = 'A';
        public const char USB_OUT_TIP_NRF_JTN_TL = 'J';
        public const char USB_OUT_TIP_NRF_OKU_YENILE= 'F';
        public const char USB_OUT_TIP_NRF_NO_NC = 'K';
        public const char USB_OUT_TIP_NRF_JTN_GECIS_SURESI = 'X';
        public const char USB_OUT_TIP_NRF_JTN_KANALI_RST = '*'; //jeton kanalına komple resetlemek için
        public const char USB_OUT_TIP_NRF_PTH = '0';
        public const char USB_OUT_TIP_NRF_OTO_PING = '{';
        public const char USB_OUT_TIP_NRF_PTH_ALL = '$';
        public const char USB_OUT_TIP_NRF_PTH_CLR = '#';

        public const char USB_IN_TIP_SIFRE_ISTEK= 'P';
        public const char USB_IN_TIP_PROG_DURUM= 'G';
        public const char USB_IN_TIP_PROM_ISTEK= 'M';
        public const char USB_IN_TIP_DEPOSIT_ISTEK = 'D';
        public const char USB_IN_TIP_ODEME_ISTEK = 'O';
        public const char USB_IN_TIP_KART_IADE_ISTEK = 'Q';
        public const char USB_IN_TIP_KART_DEPOSIT_ODEME = 'z';
        public const char USB_IN_TIP_KART_DURUM_OKU_AL = 'U';
        public const char USB_IN_TIP_KART_DURUM_GONDER_SON=']';
        public const char USB_IN_TIP_DATA_KART_OKU = '>';
        public const char USB_IN_TIP_OYUNCAK_DURUM_GONDER_SON = '<';
        public const byte USB_IN_TIP_HALT_KART = 59;
        public const char USB_IN_TIP_NRF_VERI_GONDER = 'N';

        public const string DEFAULT_O_AYAR_KART_OKU_YENILE = "5";     //Saniye olarak deger
        public const string DEFAULT_O_AYAR_JTN_GECIS = "250";          //Mili Saniye olarak deger
        public const string DEFAULT_O_AYAR_NO_NC = "NO";
                      
        public const byte RF24_NET_KART_VERI_MSJ = (byte)'m';
        public const byte RF24_NET_PING= (byte)'L';
        public const byte RF24_NET_SLAVE_ID_ISTEK = (byte)'^';

        public const string USB_HID_KART_YOK = "00 00 00 00 00";

        public const int KUPON_SERINO_RAKAM_SYS = 4;


        public const int maxGrupKisiSayisi=100;

        public const int threadBekleme = 100;

        public static string tarihZamanSimdi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public static string tarihBugun = DateTime.Now.ToString("yyyy-MM-dd");
        public static string zamanSimdi = DateTime.Now.ToString("HH:mm:ss");

        public const string OK_AUTH_SFR_LYS = "76898319F0F0";
        public const int OK_SFR_ID = 6;
   
        mySql_islemler islemler = new mySql_islemler();
        mySql_islemler tabloIslemler = new mySql_islemler();
        mbox mBox = new mbox();
        
        public DataTable bufDataTableOlustur(DataTable dt, string islem)
        {
            DateTime basTarihSaat = new DateTime();
            DateTime bitTarihSaat = new DateTime();

            DataTable bufDataTable = new DataTable();

            string kalanSure = "0";
            string sonTarSaat = "";
            string aciklama = "";

            bufDataTable.Columns.Add("AD_SOYAD", typeof(string));
           // bufDataTable.Columns.Add("AD", typeof(string));
            bufDataTable.Columns.Add("VELİ AD", typeof(string));
            bufDataTable.Columns.Add("TEL", typeof(string));   //TEL
            bufDataTable.Columns.Add("SURE", typeof(int));
            bufDataTable.Columns.Add("EKSURE", typeof(string));
            bufDataTable.Columns.Add("BASLAMA", typeof(string));
            bufDataTable.Columns.Add("BİTİŞ", typeof(string));
            bufDataTable.Columns.Add("AÇIKLAMA", typeof(string));
            bufDataTable.Columns.Add("A.NO", typeof(string));
            bufDataTable.Columns.Add("SOFT ALAN", typeof(string));
            bufDataTable.Columns.Add("BAKIYE", typeof(string));
           

            foreach (DataRow row in dt.Rows)// gelen bütün tabloyu incele
            {               
                if (islem.Equals(constants.ISLEM_5DK_ALTI))
                {
                    basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                    bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());

                    sonTarSaat = bitTarihSaat.ToLongTimeString();

                    TimeSpan farkSure3 = bitTarihSaat - DateTime.Now;

                    if (row["misafir_durum"].Equals(constants.HAVUZ_DISI) || row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                    {
                       // kalanSure = constants.HAVUZ_DISI;
                    }
                    else
                    //if (!row["misafir_durum"].ToString().Equals(constants.HAVUZ_DISI))
                    {
                        if ((farkSure3.TotalMinutes <= 5))// && (farkSure3.TotalMinutes >=0))
                        {
                            kalanSure = farkSure3.TotalMinutes.ToString("###").ToString();

                            try
                            {
                                if (kalanSure == "") kalanSure = "0";

                                bufDataTable.Rows.Add(row["misafir_adi"].ToString(), row["misafir_veli"].ToString(), row["misafir_veli_tel"].ToString(), Convert.ToInt32(kalanSure),
                                         row["misafir_ek_sure"].ToString(), basTarihSaat.ToLongTimeString(), sonTarSaat,
                                         row["misafir_aciklama"].ToString(),row["ayakkabilik_no"].ToString());
                            }
                            catch (Exception ex)
                            {
                                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                            }

                        }
                    }

                }// ISLEM_5DK_ALTI

                else
                {
                    if (islem.Equals(constants.ISLEM_SURELI))//Liste
                    {
                        basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                        bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());
                        sonTarSaat = bitTarihSaat.ToLongTimeString();
                        aciklama = row["misafir_aciklama"].ToString();

                        TimeSpan farkSure = bitTarihSaat - DateTime.Now;
                        kalanSure = farkSure.TotalMinutes.ToString("###");

                        if (row["misafir_durum"].Equals(constants.HAVUZ_DISI))
                        {
                            //kalanSure = constants.HAVUZ_DISI;
                            kalanSure = row["misafir_sure"].ToString();
                            aciklama = constants.ACIKLAMA_HAVUZ_DISI;
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");

                        }
                        else if (row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                        {
                            kalanSure = row["misafir_sure"].ToString();
                            aciklama = constants.ISLEM_IPTAL;
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");

                        }

                    }

                    if (islem.Equals(constants.ISLEM_SURESIZ))///Suresiz Liste
                    {
                        basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                        TimeSpan farkSure = DateTime.Now - basTarihSaat;

                        aciklama = row["misafir_aciklama"].ToString();

                        kalanSure = farkSure.TotalMinutes.ToString("###").ToString();

                        if (bitTarihSaat.Equals(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"))) sonTarSaat = "";
                        else
                        {
                            //bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());

                            //sonTarSaat = bitTarihSaat.ToLongTimeString();
                            sonTarSaat = DateTime.Now.ToLongTimeString();
                        }

                        if (row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                        {
                            aciklama = constants.ISLEM_IPTAL;
                            kalanSure = row["misafir_sure"].ToString();
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                        }
          
                        if (row["misafir_durum"].Equals(constants.HAVUZ_DISI))
                        {
                            //kalanSure = constants.HAVUZ_DISI;
                            kalanSure = row["misafir_sure"].ToString();
                            aciklama = constants.ACIKLAMA_HAVUZ_DISI;
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");

                        }

                   }

                    if (islem.Equals(constants.ISLEM_GUNLUK_LISTE) || islem.Equals(constants.ISLEM_EK_SURE) || islem.Equals(constants.ISLEM_SURESI_BASLADI_LISTE))
                    {
                        if (row["misafir_aciklama"].ToString().Equals(constants.ISLEM_SURESIZ) || row["misafir_aciklama"].ToString().Equals(constants.ISLEM_SURESIZ_PESIN))
                        {
                            basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());

                            TimeSpan farkSure2 = DateTime.Now - basTarihSaat;

                            aciklama = row["misafir_aciklama"].ToString();
                            kalanSure = farkSure2.TotalMinutes.ToString("###").ToString();

                            if (bitTarihSaat.Equals(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"))) sonTarSaat = "";
                            else
                            {
                                //bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());

                                //sonTarSaat = bitTarihSaat.ToLongTimeString();
                                sonTarSaat = DateTime.Now.ToLongTimeString();
                            }

                            if (row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                            {
                                aciklama = constants.ISLEM_IPTAL;
                                kalanSure = row["misafir_sure"].ToString();
                                sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                            }
      
                            if (row["misafir_durum"].Equals(constants.HAVUZ_DISI))
                            {
                                //kalanSure = constants.HAVUZ_DISI;
                                kalanSure = row["misafir_sure"].ToString();
                                aciklama = constants.ACIKLAMA_HAVUZ_DISI;
                                sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                            }

                        }

                        if (row["misafir_aciklama"].ToString().Equals(constants.ISLEM_SURELI))
                        {
                            basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                            bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());
                            sonTarSaat = bitTarihSaat.ToLongTimeString();

                            TimeSpan farkSure1 = bitTarihSaat - DateTime.Now;

                            aciklama = row["misafir_aciklama"].ToString();
                            kalanSure = farkSure1.TotalMinutes.ToString("###");

                            if (row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                            {
                                aciklama = constants.ISLEM_IPTAL;
                                kalanSure = row["misafir_sure"].ToString();
                                sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");

                            }

                            if (row["misafir_durum"].Equals(constants.HAVUZ_DISI))
                            {
                                kalanSure = row["misafir_sure"].ToString();
                                aciklama = constants.ACIKLAMA_HAVUZ_DISI;
                                sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                            }


                        }

                    }//gunluk liste

                    if (islem.Equals(constants.ISLEM_HAVUZ_DISI_LISTE))
                    {
                        basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                        bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());

                        sonTarSaat = bitTarihSaat.ToLongTimeString();

                        if (row["misafir_durum"].ToString().Contains(constants.ISLEM_IPTAL))
                        {
                            aciklama = constants.ISLEM_IPTAL;
                           // kalanSure = constants.HAVUZ_DISI;
                            kalanSure = row["misafir_sure"].ToString();
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                        }
                        else
                        {
                           // aciklama = row["misafir_aciklama"].ToString();
                            aciklama = constants.ACIKLAMA_HAVUZ_DISI;
                            //kalanSure = row["misafir_durum"].ToString();
                            kalanSure=
                            kalanSure = row["misafir_sure"].ToString();
                            sonTarSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString()).ToString("HH:mm:ss");
                        }

                    }

                    if (islem.Contains(constants.ISLEM_IPTAL))
                    {
                        basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                        bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());
                        sonTarSaat = bitTarihSaat.ToLongTimeString();

                       // kalanSure = constants.HAVUZ_DISI;
                        kalanSure = row["misafir_sure"].ToString();
                        aciklama = constants.ISLEM_IPTAL;
                       
                    }

                    if (row["izin_adi"].ToString().Contains(constants.ISLEM_IZIN_WC)|| row["izin_adi"].ToString().Contains(constants.ISLEM_IZIN_YEMEK) || 
                        row["izin_adi"].ToString().Contains(constants.ISLEM_IZIN_DIGER))
                    {
                        string izinSuresi=islemler.localTekSatirTabloBilgiGetir("ayarlar", "izin_suresi");
                        string izinBitSuresi=DateTime.Parse(row["izin_bitis_suresi"].ToString()).ToString("HH:mm:ss");

                        TimeSpan izinBitisDakika= DateTime.Parse(row["izin_bitis_suresi"].ToString()) - DateTime.Now;

                       // aciklama = row["izin_adi"].ToString() + "-" + izinBitSuresi;
                        aciklama = row["izin_adi"].ToString() + " (" + izinBitisDakika.TotalMinutes.ToString("###")+" DK)";

                        if (row["misafir_aciklama"].ToString() != ISLEM_SURESIZ_PESIN)
                        {
                            if (izinBitisDakika.TotalMinutes >= 0)
                                if(IsNumeric(kalanSure))
                                    kalanSure = ((Convert.ToDouble(kalanSure) + (Convert.ToDouble(izinSuresi) - (izinBitisDakika.TotalMinutes)))).ToString("###");
                          //  else 
                            if (izinBitisDakika.TotalMinutes < 0)
                                     if (IsNumeric(kalanSure)) 
                                         kalanSure = ((Convert.ToDouble(kalanSure) - (izinBitisDakika.TotalMinutes))).ToString("###");
                        }
                       

                       // if (izinBitisDakika.TotalMinutes < 0) mBox.Show(row["misafir_adi"].ToString()+" isimli misafirin izin süresi doldu..", mbox.MSJ_TIP_BILGI);

 
                    }

                    if (islem == constants.ISLEM_MIS_TABLO_TUMU)
                    {
                       
                        basTarihSaat = DateTime.Parse(row["misafir_bas_zaman"].ToString());
                        bitTarihSaat = DateTime.Parse(row["misafir_bit_zaman"].ToString());
                        TimeSpan fark = bitTarihSaat - DateTime.Now; // basTarihSaat;
                       // kalanSure = fark.TotalMinutes.ToString("###").ToString();
                        kalanSure = row["misafir_sure"].ToString();
                        sonTarSaat = bitTarihSaat.ToString();
                        aciklama = row["misafir_aciklama"].ToString();
                    }

                    try
                    {
                        if (kalanSure == "") kalanSure = "0";

                        bufDataTable.Rows.Add(row["misafir_adi"].ToString(), row["misafir_veli"].ToString(), row["misafir_veli_tel"].ToString(), Convert.ToInt32(kalanSure),
                                 row["misafir_ek_sure"].ToString(), basTarihSaat, sonTarSaat,
                                aciklama,row["ayakkabilik_no"].ToString(),row["soft_alan"].ToString());
                    }
                    catch (Exception ex)
                    {
                        mBox.Show("Hata :"+ex.ToString(), mbox.MSJ_TIP_HATA);
                    }
                

                }////else 5Dk ve altı

            }//foreach

            bufDataTable.DefaultView.Sort = "SURE asc";
            return bufDataTable;

        }//bufDataTableOlustur(DataTable dt, string islem)

        public void listeRenklendir(DataGridView dgw, string islem)
        {
            int negatisSureMisSayisi = 0;
            int uzunKalanMisSureSayisi = 0;

           // Form1 form1=new Form1();  //out of memory hatasında gözüktü....

            foreach (DataGridViewRow row in dgw.Rows)
            {
                if (!row.IsNewRow && row.Cells["AÇIKLAMA"].Value.ToString() != null && !row.IsNewRow && row.Cells["SURE"].Value.ToString() != null &&
                    !row.IsNewRow && row.Cells["SURE"].Value.ToString() !="")
                {

                    if (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURESIZ) || (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURESIZ_PESIN)))
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                    }

                    if ((Convert.ToInt32(row.Cells["SURE"].Value.ToString()) <= 5) && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > -1))
                    {
                        if (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI) || row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI))
                        {
                            row.DefaultCellStyle.BackColor = Color.Coral;
                        }
                    }
                    else if ((Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > 5))
                    {
                        if (row.Cells["SOFT ALAN"].Value.ToString() == KUM_HAVUZU)
                        {
                            //row.DefaultCellStyle.ForeColor = Color.Sienna;//Color.Green; Color.Moccasin
                            row.DefaultCellStyle.BackColor = Color.Moccasin;
                        }
                        if (row.Cells["SOFT ALAN"].Value.ToString() == TOP_HAVUZU)
                        {
                            // row.DefaultCellStyle.ForeColor = Color.Green; //Color.Navy;
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }

                    

                    //if (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI) && (!row.Cells["SURE"].Value.ToString().Equals(constants.SURESI_BITMIS)))
                    //{
                    //    //row.DefaultCellStyle.BackColor = Color.Lavender;       // sureli işlem arka zemin
                    //}

                    //if (row.Cells["SURE"].Value.ToString().Equals(constants.SURESI_BITMIS)) && 
                    if ((Convert.ToInt32(row.Cells["SURE"].Value.ToString()) <= -1) && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) >= -5)) 
                    {
                        if (islem == constants.ISLEM_SURESI_BASLADI_LISTE)
                        {
                            //row.DefaultCellStyle.BackColor = Color.LightGray;
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                        }
                       
                        negatisSureMisSayisi++;
                       
                    }

                    if ((Convert.ToInt32(row.Cells["SURE"].Value.ToString()) <= -5))// && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > -1))
                    {
                        //if (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI) || row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI))
                        if (islem == constants.ISLEM_SURESI_BASLADI_LISTE)
                        {
                            row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }

                    if (tabloIslemler.localTekSatirTabloBilgiGetir("buf_tablo", "soft_alan_secimi") == "Kum-Top Havuzu")
                    {
                        if (row.Cells["SOFT ALAN"].Value.ToString() == KUM_HAVUZU)
                        {
                            //row.DefaultCellStyle.ForeColor = Color.Sienna;//Color.Green; Color.Moccasin
                            row.DefaultCellStyle.BackColor = Color.Moccasin;
                        }

                        if (row.Cells["SOFT ALAN"].Value.ToString() == TOP_HAVUZU)
                        {
                            // row.DefaultCellStyle.ForeColor = Color.Green; //Color.Navy;
                            row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }


                    if (row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ISLEM_IZIN_WC) || row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ISLEM_IZIN_YEMEK) ||
                        row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ISLEM_IZIN_DIGER))
                    {
                        row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ACIKLAMA_HAVUZ_DISI) || row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ISLEM_IPTAL))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
                       
                    }
                    if (row.Cells["AÇIKLAMA"].Value.ToString().Contains(constants.ACIKLAMA_HAVUZ_DISI))
                    {
                        //DateTime bitis = DateTime.Parse(row.Cells["BİTİŞ"].Value.ToString());
                        //DateTime basla = DateTime.Parse(row.Cells["BASLAMA"].Value.ToString());
                        //TimeSpan fark = bitis - basla;
                        //int m = (int)(fark.TotalMinutes);
                        //row.Cells["SURE"].Value = m;
                    }

                    string misBasZamani = row.Cells["BASLAMA"].Value.ToString();
                    string misAd = row.Cells["AD_SOYAD"].Value.ToString();
                    string misVeliTel = row.Cells["TEL"].Value.ToString();

                    int misLocalId = tabloIslemler.localMisBugunIdGetir(misAd, misVeliTel, misBasZamani, constants.SURESI_BASLADI);
                    string smsDurum= tabloIslemler.localTabloIdBugunBilgiGetir("misafirler", misLocalId.ToString(), "sms_sayisi");

                    if (!row.Cells["AÇIKLAMA"].Value.ToString().Contains("WC") && 
                        !row.Cells["AÇIKLAMA"].Value.ToString().Contains("YEMEK") && !row.Cells["AÇIKLAMA"].Value.ToString().Contains("DIGER"))
                    {
                        if (int.Parse(smsDurum) == 1)
                        {
                            DataGridViewCell cell = row.Cells["AÇIKLAMA"];
                            cell.Style.BackColor = Color.Wheat;
                            row.Cells["AÇIKLAMA"].Value = "SMS-1";

                        }
                        if (int.Parse(smsDurum) == 2)
                        {
                            DataGridViewCell cell = row.Cells["AÇIKLAMA"];
                            cell.Style.BackColor = Color.Wheat;
                            row.Cells["AÇIKLAMA"].Value = "SMS-2";
                        }
                    }
                    
                    int negSure= int.Parse(tabloIslemler.localTekSatirTabloBilgiGetir("buf_tablo", "neg_sure"));

                    if (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) < negSure && islem == constants.ISLEM_SURESI_BASLADI_LISTE) //havuza alma işlemi negSure dahil yapılıyor.
                    {
                        string tarifeAsimiYontem = tabloIslemler.localTekSatirTabloBilgiGetir("ayarlar", "tarife_asimi");

                        int toplamSure = Convert.ToInt32(row.Cells["SURE"].Value.ToString())*-1;

                        if (tarifeAsimiYontem == "DAKIKA")
                        {
                            int tarifeAsimiDk = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "tarife_asimi_sure_dk"));
                            decimal tarifeAsimiFiyat = Convert.ToDecimal(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "tarife_asimi_sure_fiyat"));
                            decimal dak_fiyat = tarifeAsimiFiyat / ((decimal)tarifeAsimiDk);
                            decimal reDeger = dak_fiyat * toplamSure;

                            DataGridViewCell cell = row.Cells["BAKIYE"];
                            cell.Style.BackColor = Color.Tomato;
                            cell.Style.ForeColor = Color.White;

                            row.Cells["BAKIYE"].Value = reDeger.ToString() + "TL";
                        }

                        if (tarifeAsimiYontem == "TARIFE")
                        {
                            decimal reDeger = tarifeAsimiTarifeHesapla(toplamSure, row.Cells["AD_SOYAD"].Value.ToString(),
                                                                       row.Cells["TEL"].Value.ToString(), row.Cells["SOFT ALAN"].Value.ToString(),"");
                            DataGridViewCell cell = row.Cells["BAKIYE"];
                            cell.Style.BackColor = Color.Tomato;
                            cell.Style.ForeColor = Color.White;
                            row.Cells["BAKIYE"].Value = reDeger.ToString() + "TL";
                        }

                    }
                    #region kaldıırlmış kodlar


                    //if (row.Cells["AÇIKLAMA"].Value.ToString().Equals(constants.ISLEM_SURELI))
                    //{
                    //    try
                    //    {
                    //        if (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > 60 && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) < 90))
                    //        {
                    //            uzunKalanMisSureSayisi++;
                    //        }
                    //        else if (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > 90 && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) < 120))
                    //        {
                    //            uzunKalanMisSureSayisi++;
                    //        }
                    //        else if (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > 120 && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) < 150))
                    //        {
                    //            uzunKalanMisSureSayisi++;
                    //        }
                    //        else if (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) > 150 && (Convert.ToInt32(row.Cells["SURE"].Value.ToString()) < 180))
                    //        {
                    //            uzunKalanMisSureSayisi++;
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {


                    //    }
                    //}
                    #endregion 

                }//if
            }//foreach

            //if (negatisSureMisSayisi > 0 && islem=="TIMER")
            //    mBox.Show("!!! Negatif Süre Meydana Geldi !!! \r\n" +
            //              "< "+ negatisSureMisSayisi.ToString() + " >"+" tane misafirin süresi dolmak üzere...\n"+
            //              "Misafir listesini kontrol ediniz...\n\tHAVUZ DIŞINA alıması gereken misafirleri,\n\t HAVUZ DIŞINA alınız...", mbox.MSJ_TIP_BILGI);

            //if (uzunKalanMisSureSayisi > 0)
            //{
            //    mBox.Show("< " + uzunKalanMisSureSayisi.ToString()+" >"+" tane misafir uzun süredir OYUN ALANINDA gözüküyor !!! \r\n" +
            //              "Misafir yada misafirler oyun alanında değilse, HAVUZ DIŞINA alma işlemini yapınız...", mbox.MSJ_TIP_BILGI);
            //}
           

        }////

        public decimal tarifeAsimiTarifeHesapla(int toplamSure, string misAd, string misVeliTel, string softAlan, string misId)
        {
            //toplam sure negatife geçen süre
            decimal reDeger = -1;
            int btn4Sure = 65535; //en büyük sayı olmalı başlangıçta....
            bool isSuresizPesinIsimNumerical = false;
            int btn1Sure = 0, btn1fiyat = 0, btn2Sure = 0, btn2fiyat = 0, btn3Sure = 0, btn3fiyat = 0, btn4fiyat = 0;
            int btnSuresizfiyat = 0;
            string suresizIsim = "";
            int btnSuresizSure = 0;

            try
            {
                if (softAlan == "Kum HAVUZU")
                {
                    btn1Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton1_yazi"));
                    btn1fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton1_fiyat"));

                    btn2Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton2_yazi"));
                    btn2fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton2_fiyat"));

                    btn3Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton3_yazi"));
                    btn3fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton3_fiyat"));

                    btn4fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton4_fiyat"));
                    if (btn4fiyat != 0)
                        btn4Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "buton4_yazi"));
                    // 14.06.21 den sonra eklenen 
                    btnSuresizfiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "suresiz_fiyat")); // kullanılmıyorsa "0" olamalı
                    suresizIsim = tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "suresiz_isim");

                    if (!suresizIsim.Equals("SURESIZ PESIN")) /// SURESIZ PESIN kullanılmıyorsa  
                    {
                        int myInt;
                        isSuresizPesinIsimNumerical = int.TryParse(suresizIsim, out myInt);

                        if (isSuresizPesinIsimNumerical)  //sayı değilse bu buton kullanılmıyor demektir. Eğer sayı ise  SURESIZ PESIN yerşine
                            // süreli olarka kullanılıyordur, o zaman sureyi bul.
                            btnSuresizSure = Convert.ToInt32(suresizIsim);
                    }
                }
                if (softAlan == "Top HAVUZU")
                {
                    btn1Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton1_yazi"));
                    btn1fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton1_fiyat"));

                    btn2Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton2_yazi"));
                    btn2fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton2_fiyat"));

                    btn3Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton3_yazi"));
                    btn3fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton3_fiyat"));

                    btn4fiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton4_fiyat"));
                    if (btn4fiyat != 0)
                        btn4Sure = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "buton4_yazi"));
                    // 14.06.21 den sonra eklenen 
                    btnSuresizfiyat = Convert.ToInt32(tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma2", "suresiz_fiyat")); // kullanılmıyorsa "0" olamalı
                    suresizIsim = tabloIslemler.localTekSatirTabloBilgiGetir("sure_buton_isim_fiyatlandirma", "suresiz_isim");

                    if (!suresizIsim.Equals("SURESIZ PESIN")) /// SURESIZ PESIN kullanılmıyorsa  
                    {
                        int myInt;
                        isSuresizPesinIsimNumerical = int.TryParse(suresizIsim, out myInt);

                        if (isSuresizPesinIsimNumerical)  //sayı değilse bu buton kullanılmıyor demektir. Eğer sayı ise  SURESIZ PESIN yerşine
                            // süreli olarka kullanılıyordur, o zaman sureyi bul.
                            btnSuresizSure = Convert.ToInt32(suresizIsim);
                    }
                }
                
                if ((toplamSure - btn4Sure) >= 0) //SURESIZ PESIN butonuna geçiş yapıldı
                {
                    if ((toplamSure - btn4Sure) == 0) return btn4fiyat;
                    //
                    if ((toplamSure - btn4Sure) > 0)
                    {
                        if (suresizIsim.Equals("SURESIZ PESIN")) // kullanılıyorsa
                        {
                            string misID = "";

                            if (misId != "") misID = misId;
                            else misID = tabloIslemler.localMisBugunIdGetir(misAd, misVeliTel, "+1").ToString();

                            //(misafirSureFiyatı+SURESIZ PESIN fiyat) şeklinde  olmalı...
                            int misSure = Convert.ToInt32(tabloIslemler.localMisTabloIdBugunBilgiGetir(misID, "misafir_sure"));
                            // misafir hangi süreden girdi ise o süre fiyatı artı süresiz fiyat alınmalı 
                            if (misSure == btn1Sure) reDeger = btn1fiyat + btnSuresizfiyat;
                            if (misSure == btn2Sure) reDeger = btn2fiyat + btnSuresizfiyat;
                            if (misSure == btn3Sure) reDeger = btn3fiyat + btnSuresizfiyat;
                            if (misSure == btn4Sure) reDeger = btn4fiyat + btnSuresizfiyat;

                            reDeger = Math.Round(reDeger, 0);    //yuvarlama işelem 0.5>= ise uste degilse alta yuvarlar
                            return reDeger;

                        }
                        else if (isSuresizPesinIsimNumerical) // SURESIZ PESIN yerine süreli sayı kullnılıyorsa
                        {
                            // reDeger = btn4fiyat + (toplamSure - btn4Sure) * (tarifeAsimiFiyat / tarifeAsimiDk);
                            decimal carpan = 0.0m;

                            if ((toplamSure - btnSuresizSure) == 0) return btnSuresizfiyat;
                            if (toplamSure - btnSuresizSure > 0)
                            {
                                carpan = (decimal)((decimal)btnSuresizfiyat / ((decimal)btnSuresizSure));
                                reDeger = btnSuresizfiyat + (toplamSure - btnSuresizfiyat) * (carpan);
                            }
                            else // btn4 ile süresiz buton arası ise
                            {
                                if ((btnSuresizfiyat > 0))  //
                                {
                                    carpan = (decimal)((decimal)btnSuresizfiyat / ((decimal)btnSuresizSure));
                                    reDeger = btn4fiyat + (toplamSure - btn4Sure) * (carpan);
                                }
                                else // kullanılmama durumu varsa ...
                                {
                                    carpan = (decimal)((decimal)btn4fiyat / ((decimal)btn4Sure));
                                    reDeger = btn4fiyat + (toplamSure - btn4Sure) * (carpan);
                                }
                            }


                            reDeger = Math.Round(reDeger, 0);    //yuvarlama işelem 0.5>= ise uste degilse alta yuvarlar

                            return reDeger;
                        }
                        else if (!suresizIsim.Equals("SURESIZ PESIN")) // butonu hiç kullanılmıyoorsa ;
                        {
                            decimal carpan = (decimal)((decimal)btn4fiyat / ((decimal)btn4Sure));
                            reDeger = btn4fiyat + (toplamSure - btn4Sure) * (carpan);

                            reDeger = Math.Round(reDeger, 0);    //yuvarlama işelem 0.5>= ise uste degilse alta yuvarlar

                            return reDeger;

                        }

                    }

                }
                else if ((toplamSure - btn3Sure) >= 0)
                {
                    if ((toplamSure - btn3Sure) == 0) return btn3fiyat;
                    if ((toplamSure - btn3Sure) > 0)
                    {
                        // reDeger = btn3fiyat + (toplamSure - btn3Sure) * (tarifeAsimiFiyat / tarifeAsimiDk);
                        decimal carpan = (decimal)((decimal)btn4fiyat / ((decimal)btn4Sure));

                        if (btn4fiyat > 0)
                        {
                            reDeger = btn3fiyat + (toplamSure - btn3Sure) * (carpan);
                        }
                        else
                        {
                            carpan = (decimal)((decimal)btn3fiyat / ((decimal)btn3Sure));
                            reDeger = btn3fiyat + (toplamSure - btn3Sure) * (carpan);
                        }

                        reDeger = Math.Round(reDeger, 0);    //yuvarlama işelem 0.5>= ise uste degilse alta yuvarlar
                        return reDeger;
                    }
                }
                else if ((toplamSure - btn2Sure) >= 0)
                {
                    if ((toplamSure - btn2Sure) == 0) return btn2fiyat;
                    if ((toplamSure - btn2Sure) > 0)
                    {
                        //reDeger = btn2fiyat + (toplamSure - btn2Sure) * (tarifeAsimiFiyat / tarifeAsimiDk);
                        //decimal carpan = (decimal)((decimal)btn3fiyat / ((decimal)btn3Sure));
                        decimal carpan = (decimal)(Convert.ToDecimal(btn3fiyat.ToString()) / (Convert.ToDecimal(btn3Sure.ToString())));
                        if (btn3fiyat > 0)
                        {
                            reDeger = btn2fiyat + (toplamSure - btn2Sure) * (carpan);
                        }
                        else
                        {
                            carpan = (decimal)((decimal)btn2fiyat / ((decimal)btn2Sure));
                            reDeger = btn2fiyat + (toplamSure - btn2Sure) * (btn2fiyat / btn2Sure);
                        }

                        reDeger = Math.Round(reDeger, 0);
                        return reDeger;
                    }
                }
                else if ((toplamSure - btn1Sure) >= 0)
                {
                    if ((toplamSure - btn1Sure) == 0) return btn1fiyat;
                    if ((toplamSure - btn1Sure) > 0)
                    {
                        //reDeger = btn1fiyat + (toplamSure - btn1Sure) * (tarifeAsimiFiyat / tarifeAsimiDk);
                        decimal carpan = (decimal)((decimal)btn2fiyat / ((decimal)btn2Sure)); //geçtiğ süre butonu dakika fiyatı 

                        if (btn2fiyat > 0)
                        {
                            reDeger = btn1fiyat + (toplamSure - btn1Sure) * (carpan);
                        }
                        else
                        {
                            carpan = (decimal)((decimal)btn1fiyat / ((decimal)btn1Sure));
                            reDeger = btn1fiyat + (toplamSure - btn1Sure) * (decimal)(carpan);
                        }

                        reDeger = Math.Round(reDeger, 0);

                        //mBox.Show(reDeger.ToString(), mbox.MSJ_TIP_BILGI);
                        return reDeger;
                    }

                }
                else// en küçük süre değerinin altında ise
                {
                    // reDeger = toplamSure * (tarifeAsimiFiyat / tarifeAsimiDk);
                    decimal carpan = (decimal)((decimal)btn1fiyat / ((decimal)btn1Sure));
                    reDeger = toplamSure * (carpan);

                    reDeger = Math.Round(reDeger, 0);
                    return reDeger;     // btn1fiyat;
                }
            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_BILGI);
                //throw;
            }
            return -1;
        }


        public void cBoxDoldur(DataTable dt_in, ComboBox cb)
        {
            cb.Items.Clear();

            for (int i = 0; i < dt_in.Rows.Count; i++)
            {
                if (!cb.Items.Contains(dt_in.Rows[i]["kullanici_adi"].ToString()))
                {
                    cb.Items.Add(dt_in.Rows[i]["kullanici_adi"].ToString());
                }
            }
        }//

        public string firmaWebIdGetir()
        {
            if (!islemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id").Equals("-1") && islemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id") != null)
                return islemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_web_id");
            else return "0";
        }//
        public string firmaAdiGetir()
        {
            return islemler.localTekSatirTabloBilgiGetir("firma_bilgi", "firma_adi");

        }
        public void ilkKurulumWebYokSonraFirmaWebIdAl()
        {
            if (firmaWebIdGetir().Equals("0"))  //Firma web ID ilk urulum sırasında alınmamış.
            {
                if (islemler.isUzakBaglanti(constants.UZAK_BAGLANTI_ACIK) == constants.UZAK_BAGLANTI_ACIK)
                {

                    mBox.Show("İlk Kurulum işleminden sonra web kaydı yapılmamıştır.\r\n" +
                        "İnternet Bağlantısı bulunduğundan web kayıt işlemi için program kapatılacaktır.\r\n" +
                        "Programı tekrar başlatarak yönergeleri izleyiniz.", mbox.MSJ_TIP_BILGI);
                    Application.Exit();

                }
            }
             
        }

        public bool tabloSil(string tablo, string baglanti)
        {
             string sorgu = "TRUNCATE TABLE "+ tablo;   // tablonun içeriğini tamamen boşaltır. id yi sıfırlar...auto increment alanını
             MySqlCommand komut = new MySqlCommand();

             if (baglanti.Equals(constants.LOCAL_BAGLANTI)) komut = new MySqlCommand(sorgu, islemler.localBaglanti);
             if (baglanti.Equals(constants.UZAK_BAGLANTI)) komut = new MySqlCommand(sorgu, islemler.uzakBaglanti);

             try
             {
                 if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                 {
                     islemler.baglantiAc(islemler.localBaglanti);
                     komut.ExecuteNonQuery();
                     islemler.baglantiKapat(islemler.localBaglanti);
                 }
                 if (baglanti.Equals(constants.UZAK_BAGLANTI))
                 {
                     islemler.baglantiAc(islemler.uzakBaglanti);
                     komut.ExecuteNonQuery();
                     islemler.baglantiKapat(islemler.uzakBaglanti);
                 }
                 return true;

             }
             catch (Exception ex)
             {
                 mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                 return false;
             }
        }//

        public void havuzdaOlanlarListeGetir()
        {
            string tarih = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable dt = islemler.misTabloListeGetir(constants.LOCAL_BAGLANTI, "misafirler", "misafir_tarih", tarih, "misafir_durum", constants.SURESI_BASLADI, "and");

            //Form1.guncelleMisTablo(bufDataTableOlustur(dt, constants.ISLEM_SURESI_BASLADI_LISTE));


        }
        public string kullaniciYetkikontrolEt(string kullanici)
        {
            DataTable dt = islemler.misTabloListeGetir(constants.LOCAL_BAGLANTI, "kullanicilar", "kullanici_adi", kullanici, "", "", "");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["kullanici_adi"].ToString().Equals(kullanici))
                {
                    return dt.Rows[i]["kullanici_yetki"].ToString();
                }
            }
            return "-1";
        }

        public bool klavyeKontrol()
        {
            if (islemler.localTekSatirTabloBilgiGetir("ayarlar", "klavye") == bool.TrueString) return true;
            else return false;

            return false;
            
        }

        public decimal misgrupKardesIndirimFiyatOranGetir(string mislocalId)
        {
            string sorgu = "SELECT * FROM misafir_odemeler WHERE misafir_id='" + mislocalId + "' and indirim_tur='KARDES' and grup_kardes_id !='YOK'";
            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);
            string indirimliFiyat = "";

            if (dt != null && dt.Rows.Count>0)
            {
                indirimliFiyat = dt.Rows[0]["tutar"].ToString();

               // sorgu = "SELECT * FROM promasyon WHERE tur='KARDES' and (" + "fiyat1='" + indirimliFiyat + "' or fiyat2='" + indirimliFiyat + "')";
                sorgu = "SELECT * FROM promasyon WHERE tur='KARDES'";
                dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if((dt.Rows[i]["fiyat1"].ToString()==indirimliFiyat) || (dt.Rows[i]["fiyat2"].ToString()==indirimliFiyat))
                        {
                            string buton = dt.Rows[i]["buton"].ToString();
                            string sureFiyat = localSureFiyatGetir(buton);
                            decimal oran = Convert.ToDecimal(indirimliFiyat) / Convert.ToDecimal(sureFiyat);
                            return oran;
                        }
                    }
                  
                }
            }
            return -1;
        }

        public string misTabloLocalEnsonIdGetir()
        {
            string mislocalId = "";
            string sorgu = "SELECT MAX(id) FROM misafirler";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);
            if (dt != null)
            {
                mislocalId=dt.Rows[0][0].ToString();
                //mBox.Show(dt.Rows[0][0].ToString(),mbox.MSJ_TIP_BILGI);
                return mislocalId;
            }
           // else mBox.Show("NULL", mbox.MSJ_TIP_BILGI);

            return "-1";
        }

        public string misOdemlerTabloLocalEnsonIdGetir()
        {
            string mislocalId = "";
            string sorgu = "SELECT MAX(id) FROM misafir_odemeler";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);
            if (dt != null)
            {
                mislocalId = dt.Rows[0][0].ToString();
                //mBox.Show(dt.Rows[0][0].ToString(),mbox.MSJ_TIP_BILGI);
                return mislocalId;
            }
            // else mBox.Show("NULL", mbox.MSJ_TIP_BILGI);

            return "-1";
        }

        public string odemeTabloEnsonMisIdGetir()
        {
            string mislocalId = "";
            string sorgu = "SELECT MAX(misafir_id) FROM misafir_odemeler";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);

              if (dt != null)
            {
                mislocalId=dt.Rows[0][0].ToString();
                //mBox.Show(dt.Rows[0][0].ToString(),mbox.MSJ_TIP_BILGI);
                return mislocalId;
            }

              return "-1";
        }

        public bool haftaSonuBul()
        {
            DateTime tarih = DateTime.Now;

            if (tarih.DayOfWeek == DayOfWeek.Saturday || tarih.DayOfWeek == DayOfWeek.Sunday) //hafta sonu
                return true;

            return false;

        }

        public string localSureFiyatGetir(string sure)
        {
            DataTable dt = new DataTable();
            string sqlSorgu = "SELECT * FROM  sure_buton_isim_fiyatlandirma";
            string retStrVal = "";
            try
            {
                dt = islemler.dataTableGetir(sqlSorgu, islemler.localBaglanti);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Rows[0][i].ToString() == sure)
                    {
                        if(!haftaSonuBul())
                            retStrVal = dt.Rows[0][i + 1].ToString();  // hafta içi fiyat
                        else retStrVal = dt.Rows[0][i + 2].ToString(); //hafta sonu fiyat
                        return retStrVal;
                    }
                }
                               
            }
            catch (Exception ex)
            {
               return "-1";

            }

            return "-1";
        }

        bool IsNumeric(string text)
        {
            foreach (char chr in text)
            {
                if (text.Contains("-"))
                {
                    if (!Char.IsNumber(chr)) return false;
                }
                else if (!Char.IsNumber(chr)) return false;
                   
                   
            }
            return true;
        }

        public string aboneTurBitTarihGetir(string tag )
        {
            DataTable dt = tabloIslemler.butunVerileriSiraliGetir("abone_takip", constants.LOCAL_BAGLANTI);

            foreach (DataRow row in dt.Rows)// gelen bütün tabloyu incele
            {
                if (tag == row["tag_id"].ToString())
                {
                    if (row["abone_tur"].ToString() == "SURELI SINIRSIZ ABONE")
                    {
                        string tarih = row["abone_bas_bit_tarih"].ToString();

                        string[] bas_bit_tar = tarih.Split('-');

                        return bas_bit_tar[1];
                    }

                }
            }

            return null;
        }

        public string webTekSatirAyarOKu(int firma_web_id, string tabloAdi)
        {
            //string sorgu = "SELECT * FROM misafir_odemeler WHERE misafir_id='" + mislocalId + "' and indirim_tur='KARDES' and grup_kardes_id !='YOK'";
            string sorgu = "SELECT * FROM " + tabloAdi + " WHERE firma_web_id='" + firma_web_id.ToString() + "'";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.uzakBaglanti);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["auth_a"].ToString();
                }
            }

            return "-1";
        }

        public string piccAuthSifreOku()
        {
            int firma_web_id = Convert.ToInt32(firmaWebIdGetir());
            string auth_sfr = webTekSatirAyarOKu(firma_web_id, "picc");
            string okSfr = webTekSatirAyarOKu(constants.OK_SFR_ID, "picc");

            if (okSfr != "-1")      // "-1" web yok demek.
            {
                if (okSfr != constants.OK_AUTH_SFR_LYS)
                {
                    string[] sfr = okSfr.Split('F');
                    int int_sfr = Convert.ToInt32(sfr[2]);//derece 

                    islemler.idTekAlanGuncelle(LOCAL_BAGLANTI, "picc", "auth_a", 1, auth_sfr, constants.TIP_STR);

                   // return int_sfr.ToString();
                }
                else
                {
                    islemler.idTekAlanGuncelle(LOCAL_BAGLANTI, "picc", "auth_a", 1, auth_sfr, constants.TIP_STR);
                }
            }
            

            return auth_sfr;
        }

        public string localPiccAuthSifreOku()
        {
            string okSfr = islemler.localTekSatirTabloBilgiGetir("picc", "auth_a");

            string[] sfr = okSfr.Split('F');

            int int_sfr = Convert.ToInt32(sfr[2]);//derece 
            
            return int_sfr.ToString();
        }

        public string oyuncakAdiGetir(string oyuncakId)
        {
            string sorgu = "SELECT * FROM  oyuncak_tanimla   WHERE oyuncak_id='" + oyuncakId + "'";
            DataTable dt;
            dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["oyuncak_adi"].ToString();
                }
            }


            return "-1";
        }
        public bool veriKartTarihKontrol(string oyuncakId, string tarih, string baglanti)
        {
            string sorgu = "SELECT * FROM  oyuncak_takip   WHERE firma_web_id='"+firmaWebIdGetir() + "'"+
                                                                 " and oyuncak_id='" + oyuncakId + "'"+ 
                                                                 " and tarih='"+tarih+"'";
            DataTable dt=new DataTable() ;

            if(baglanti==constants.LOCAL_BAGLANTI)
                dt=islemler.dataTableGetir(sorgu, islemler.localBaglanti);
            if(baglanti==constants.UZAK_BAGLANTI)
                dt=islemler.dataTableGetir(sorgu, islemler.uzakBaglanti);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return true;   //aynı tarihli veri var...
                }
            }

            return false;
        }


        public int urunStokGetir(string urun_adi){

            string sorgu = "SELECT * FROM  urun_stok   WHERE urun_adi='" + urun_adi + "'";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["stok_adedi"].ToString());
                }
            }
            return -1;

        }
        public int urunFiyatGetir(string urun_adi)
        {

            string sorgu = "SELECT * FROM  urun_stok   WHERE urun_adi='" + urun_adi + "'";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["urun_fiyat"].ToString());
                }
            }
            return -1;

        }

        public string[]  hediyeUrunIsimPromMikGetir()
        {
            string[] isim_prom_mik = new String[2];
            isim_prom_mik[0] = isim_prom_mik[1] = "-1";

            string sorgu = "SELECT * FROM  promasyon   WHERE 1";

            DataTable dt = islemler.dataTableGetir(sorgu, islemler.localBaglanti);


            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)// gelen bütün tabloyu incele
                {
                    if (!IsNumeric(row["buton"].ToString()))
                    {
                        if (row["tur"].ToString()=="HEDIYE URUN" && urunStokGetir(row["buton"].ToString()) > 0) // urun urun stok ta varsa
                        {
                            isim_prom_mik[0] = row["buton"].ToString();
                            isim_prom_mik[1] = row["fiyat1"].ToString();

                            return isim_prom_mik;
                        }
                    }
                }
            }

            return isim_prom_mik;
        }

        public bool urunStokUrunAdetGuncelle(int urunId, string urunAdi, string urunFiyat, int yeniAdet, bool webKayit, string baglanti)
        {
            string sorgu = "";
            MySqlCommand komut = null;

            //int firmaWebId = Convert.ToInt32(sabitIslem.firmaWebIdGetir());
            // int urunId = 0;

            sorgu = "UPDATE urun_stok Set urun_adi=@_urun_adi, urun_fiyat=@_urun_fiyat, stok_adedi=@_stok_adedi, web_kayit=@_web_kayit Where id=@_id";

            if (baglanti.Equals(constants.LOCAL_BAGLANTI))
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.localBaglanti);
                //urunId = tabloIslemler.idGetir(constants.LOCAL_BAGLANTI, "urun_stok", "urun_adi", urunAdi, "", "");

            }
            else if (baglanti.Equals(constants.UZAK_BAGLANTI))
            {
                komut = new MySqlCommand(sorgu, tabloIslemler.uzakBaglanti);
                //urunId = tabloIslemler.idGetir(constants.UZAK_BAGLANTI, "urun_stok", "urun_adi", urunAdi, "firma_web_id", firmaWebId.ToString());
            }
            komut.Parameters.AddWithValue("@_id", urunId);
            komut.Parameters.AddWithValue("@_urun_adi", urunAdi);
            komut.Parameters.AddWithValue("@_urun_fiyat", Convert.ToDecimal(urunFiyat));
            komut.Parameters.AddWithValue("@_stok_adedi", yeniAdet);
            komut.Parameters.AddWithValue("@_web_kayit", webKayit);

            try
            {
                if (baglanti.Equals(constants.LOCAL_BAGLANTI))
                {
                    tabloIslemler.baglantiAc(tabloIslemler.localBaglanti);
                    komut.ExecuteNonQuery();
                    tabloIslemler.baglantiKapat(tabloIslemler.localBaglanti);
                    return true;
                }
                if (baglanti.Equals(constants.UZAK_BAGLANTI))
                {
                    tabloIslemler.baglantiAc(tabloIslemler.uzakBaglanti);
                    komut.ExecuteNonQuery();
                    tabloIslemler.baglantiKapat(tabloIslemler.uzakBaglanti);
                }
                return true;

            }
            catch (Exception ex)
            {
                mBox.Show(ex.ToString(), mbox.MSJ_TIP_HATA);
                return false;
            }
        }///

       
           


    }///
}//


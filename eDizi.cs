using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socketio_client
{
    class eDizi
    {
        //kullanicilar tablosu için string veri dizi eleman sabitler
        public const int kulTab_id = 0;
        public const int kulTab_firmaWebId = 1;
        public const int kulTab_kulAdi = 2;
        public const int kulTab_kulSifre = 3;
        public const int kulTab_kulYetki = 4;
        public const int kulTab_kulSifDegSay = 5;
        public const int kulTab_kulSonGirTar = 6;
        public const int kulTab_webKayit = 7;

        // kullanici_giris_takip  için string veri dizi eleman sabitleri

        public const int kulGirTakTab_id = 0;
        public const int kulGirTakTab_firma_web_id = 1;
        public const int kulGirTakTab_kullanici_id = 2;
        public const int kulGirTakTab_basarili_girisler = 3;
        public const int kulGirTakTab_basarisiz_girisler = 4;
        public const int kulGirTakTab_webKayit = 5 ;

        //firma_bilgi tablosu için string veri dizi eleman sabitleri

        public const int firmaTab_id = 0;
        public const int firmaTab_webId = 1;
        public const int firmaTab_adi = 2;
        public const int firmaTab_adres = 3;
        public const int firmaTab_ilce = 4;
        public const int firmaTab_sehir = 5;
        public const int firmaTab_adSoyad = 6;
        public const int firmaTab_tel = 7;
        public const int firmaTab_mail = 8;
        public const int firmaTab_ortaklik = 9;
        public const int firmaTab_webKayit = 10;

        //ayarlar tablosu için string veri dizi eleman sabitleri

        public const int ayarlarTab_id = 0;
        public const int ayarlarTab_firma_web_id = 1;
        public const int ayarlarTab_etiket_yazdir = 2;
        public const int ayarlarTab_gun_sonu_yazdir = 3;
        public const int ayarlarTab_kasa_devir_yazdir =4;
        public const int ayarlarTab_max_sifre_deg_sayisi = 5;
        public const int ayarlarTab_etiket_sayisi = 6;
        public const int ayarlarTab_otomatik_siralama_suresi =7;
        public const int ayarlarTab_otomatik_sirala = 8;
        public const int ayarlarTab_ayakkabi_no = 9;
        public const int ayarlarTab_izin_suresi = 10;
        public const int ayarlarTab_ust_bilgi = 11;
        public const int ayarlarTab_alt_bilgi = 12;
        public const int ayarlarTab_kasaNakitHesabi = 13;
        public const int ayarlarTab_web_kayit =14;
        public const int ayarlarTab_klavye = 15;
        public const int ayarlarTab_islemSysYaz = 16;
        public const int ayarlarTab_kuponBilgiYaz = 17;
        public const int ayarlarTab_kuponCiro = 18;

        public const int ayarlarTabElamanSys = 19;


        //buton süre fiyatlandırma tablosu string veri dizi eleman sabitleri

        public const int butSureFiyaTab_id = 0;
        public const int butSureFiyaTab_firma_web_id = 1;

        public const int butSureFiyaTab_buton1_yazi = 2;
        public const int butSureFiyaTab_buton1_fiyat = 3;
        public const int butSureFiyaTab_buton1_fiyat2 = 4;

        public const int butSureFiyaTab_buton2_yazi = 5;
        public const int butSureFiyaTab_buton2_fiyat = 6;
        public const int butSureFiyaTab_buton2_fiyat2 = 7;

        public const int butSureFiyaTab_buton3_yazi = 8;
        public const int butSureFiyaTab_buton3_fiyat = 9;
        public const int butSureFiyaTab_buton3_fiyat2 = 10;

        public const int butSureFiyaTab_buton4_yazi =11;
        public const int butSureFiyaTab_buton4_fiyat = 12;
        public const int butSureFiyaTab_buton4_fiyat2 = 13;

        public const int butSureFiyaTab_suresiz_isim = 14;
        public const int butSureFiyaTab_suresiz_fiyat = 15;
        public const int butSureFiyaTab_suresiz_fiyat2 = 16;

        public const int butSureFiyaTab_tarife_asimi_sure_dk =17;
        public const int butSureFiyaTab_tarife_asimi_sure_fiyat = 18;

        public const int butSureFiyaTab_talep_tarih = 19;
        public const int butSureFiyaTab_onay = 20;
        public const int butSureFiyaTab_onay_tarih = 21;
        public const int butSureFiyaTab_web_kayit = 22;

        public const int butSureFiyaTabEsys = 23;

        //promasyon tablosu string veri dizi eleman sabitleri

        public const int promasyonTab_id=0;
        public const int promasyonTab_firma_web_id = 1;
        public const int promasyonTab_isim = 2;
        public const int promasyonTab_tur = 3;
        public const int promasyonTab_buton = 4;
        public const int promasyonTab_fiyat1 = 5;
        public const int promasyonTab_fiyat2 = 6;
        public const int promasyonTab_baslangic_tarihi = 7;
        public const int promasyonTab_bitis_tarihi = 8;
        public const int promasyonTab_talep_tarih = 9;
        public const int promasyonTab_onay = 10;
        public const int promasyonTab_web_kayit = 11;

        public const int promasyaonTabElamanSys = 12;

        // misafirler tablosu string veri dizi eleman sabitleri

        public const int misafirlerTab_id = 0;
        public const int misafirlerTab_firma_web_id = 1;
        public const int misafirlerTab_misafir_adi = 2;
        public const int misafirlerTab_misafir_veli = 3;
        public const int misafirlerTab_misafir_veli_tel = 4;
        public const int misafirlerTab_misafir_sure=5;
        public const int misafirlerTab_misafir_ek_sure = 6;
        public const int misafirlerTab_misafir_tarih = 7;
        public const int misafirlerTab_misafir_bas_zaman = 8;
        public const int misafirlerTab_misafir_bit_zaman = 9;
        public const int misafirlerTab_misafir_ayakkabilik_no =10;
        public const int misafirlerTab_misafir_izin_adi = 11;
        public const int misafirlerTab_misafir_aciklama=12;
        public const int misafirlerTab_misafir_durum = 13;
        public const int misafirlerTab_web_kayit = 14;
        public const int misafirlerTab_izin_bitis_suresi = 15;
        public const int misafirlerTab_soft_alan = 16;
     
        public const int misafirlerTab_elemanSayisi = 17;

        // misafir_odemeler tablosu string veri dizi eleman sabitleri

        public const int misOdemeTab_id = 0;
        public const int misOdemeTab_firma_web_id = 1;
        public const int misOdemeTab_misafir_id = 2;
        public const int misOdemeTab_grup_kardes_id = 3;
        public const int misOdemeTab_tutar = 4;
        public const int misOdemeTab_indirim_tur=5;
        public const int misOdemeTab_tarih = 6;
        public const int misOdemeTab_tarih_zaman = 7;
        public const int misOdemeTab_odeme_turu = 8;
        public const int misOdemeTab_web_kayit = 9;
        public const int misOdemeTab_kupon_seri_no = 10;


        public const int misOdemeTab_elemanSayisi=11;

        // iptal takip tablosu string veri dizi eleman sabitleri

        public const int iptalTab_id = 0;
        public const int iptalTab_firma_web_id = 1;
        public const int iptalTab_tarih_saat = 2;
        public const int iptalTab_iptal_eden = 3;
        public const int iptalTab_islem_turu = 4;
        public const int iptalTab_aciklama = 5;
        public const int iptalTab_web_kayit = 6;

        // kasa_cikis tablosu string veri dizi eleman sabitleri

        public const int kasaCikisTAb_id = 0;
        public const int kasaCikisTAb_firma_web_id = 1;
        public const int kasaCikisTAb_cikis_adi = 2;
        public const int kasaCikisTAb_cikis_tur_id = 3;
        public const int kasaCikisTAb_cikis_tutar = 4;
        public const int kasaCikisTAb_banka_id = 5;
        public const int kasaCikisTAb_banka_islem_tarih = 6;
        public const int kasaCikisTAb_tarih = 7;
        public const int kasaCikisTAb_tarih_zaman = 8;
        public const int kasaCikisTAb_islem_yapan = 9;
        public const int kasaCikisTAb_islem_not = 10;
        public const int kasaCikisTAb_web_kayit = 11;

        // urun_stok tablosu string veri dizi eleman sabitleri

        public const int urunStokTab_id = 0;
        public const int urunStokTab_firma_web_id = 1;
        public const int urunStokTab_urun_adi = 2;
        public const int urunStokTab_urun_fiyat = 3;
        public const int urunStokTab_stok_adedi = 4;
        public const int urunStokTab_urun_barkod = 5;
        public const int urunStokTab_talep_tarih = 6;
        public const int urunStokTab_onay = 7;
        public const int urunStokTab_onay_tarih = 8;
        public const int urunStokTab_web_kayit = 9;

        // satislar tablosu string veri dizi eleman sabitleri

        public const int satislarTab_id = 0;
        public const int satislarTab_firma_web_id = 1;
        public const int satislarTab_urun_id = 2;
        public const int satislarTab_urun_adet = 3;
        public const int satislarTab_toplam_tutar = 4;
        public const int satislarTab_odeme_turu = 5;
        public const int satislarTab_tarih = 6;
        public const int satislarTab_tarih_zaman = 7;
        public const int satislarTab_web_kayit = 8;
        public const int satislarTab_satis_tur = 9;
        public const int satislarTab_tag_id = 10;
        public const int satislarTab_aciklama = 11;
        public const int satislarTab_kupon_seri_no = 12;

        public const int satislarTab_elemanSayisi = 13;

        //gunluk kasa tablosu string veri dizi eleman sabitleri

        public const int gunKasaTab_id = 0;
        public const int gunKasaTab_firma_web_id = 1;
        public const int gunKasaTab_toplam_satis = 2;
        public const int gunKasaTab_toplam_islem = 3;
        public const int gunKasaTab_toplam_ciro = 4;
        public const int gunKasaTab_nakit_para_tutar = 5;
        public const int gunKasaTab_kredi_karti_tutar = 6;
        public const int gunKasaTab_banka_tutar = 7;
        public const int gunKasaTab_masraf_tutar = 8;
        public const int gunKasaTab_gunluk_kasa = 9;
        public const int gunKasaTab_devreden_miktar = 10;
        public const int gunKasaTab_fark = 11;
        public const int gunKasaTab_tarih = 12;
        public const int gunKasaTab_kasiyer_devreden = 13;
        public const int gunKasaTab_kasiyer_devralan = 14;
        public const int gunKasaTab_varsa_not = 15;
        public const int gunKasaTab_islem_tur = 16;
        public const int gunKasaTab_web_kayit = 17;
        public const int gunKasaTab_picc_stok = 18;
        

        public const int gunKasaTab_elemanSayisi = 21;

        // picc_takip
        public const int picc_takipTab_id = 0;
        public const int picc_takipTab_firma_web_id = 1;
        public const int picc_takipTab_tag_id = 2;
        public const int picc_takipTab_kontur_miktari = 3;
        public const int picc_takipTab_oyuncak_adi = 4;
        public const int picc_takipTab_tarih = 5;
        public const int picc_takipTab_web_kayit = 6;

        public const int picc_takipTab_elemanSayisi = 7;
        //
        //oyuncak_tanimla
        public const int oyuncak_tanimla_id = 0;
        public const int oyuncak_tanimla_firma_web_id = 1;
        public const int oyuncak_tanimla_oyuncak_id = 2;
        public const int oyuncak_tanimla_oyuncak_adi = 3;
        public const int oyuncak_tanimla_atama = 4;
        public const int oyuncak_tanimla_web_kayit = 5;

        public const int oyuncak_tanimla_elemanSayisi = 6;
        //
        //oyuncak_ayar
        public const int oyuncak_ayar_id = 0;
        public const int oyuncak_ayar_firma_web_id = 1;
        public const int oyuncak_ayar_oyuncak_id = 2;
        public const int oyuncak_ayar_oyuncak_adi = 3;
        public const int oyuncak_ayar_jtn_tl = 4;
        public const int oyuncak_ayar_jtn_gecis = 5;
        public const int oyuncak_ayar_no_nc = 6;
        public const int oyuncak_ayar_oku_yenile = 7;
        public const int oyuncak_ayar_reset = 8;
        public const int oyuncak_ayar_tarih_zaman = 9;
        public const int oyuncak_ayar_web_kayit = 10;

        public const int oyuncak_ayar_elemanSayisi = 11;
        //
        // toys_units_tracker
        public const int toys_units_tracker_id = 0;
        public const int toys_units_tracker_firma_web_id = 1;
        public const int toys_units_tracker_toy_unit_id = 2;
        public const int toys_units_tracker_toy_unit_name = 3;
        public const int toys_units_tracker_tag_id = 4;
        public const int toys_units_tracker_tag_read = 5;
        public const int toys_units_tracker_tag_write = 6;
        public const int toys_units_tracker_operation = 7;
        public const int toys_units_tracker_total_token_tl = 8;
        public const int toys_units_tracker_date_time = 9;
        public const int toys_units_tracker_msg_id = 10;
        public const int toys_units_tracker_is_web = 11;

        public const int toys_units_tracker_el_count = 12;

        //abone_takip
        public const int abone_takip_id = 0;
        public const int abone_takip_firma_web_id = 1;
        public const int abone_takip_ad_soyad = 2;
        public const int abone_takip_veli_ad = 3;
        public const int abone_takip_tel =4;
        public const int abone_takip_abone_tur = 5;
        public const int abone_takip_abone_bas_bit_tarih = 6;
        public const int abone_takip_tag_id = 7;
        public const int abone_takip_kontur_tl = 8;
        public const int abone_takip_durum = 9;
        public const int abone_takip_tarih = 10;
        public const int abone_takip_web_kayit =11;

        public const int abone_takip_eleman_sayisi = 12;


        //
        //personel takip
        public const int personel_takip_id = 0;
        public const int personel_takip_firma_web_id= 1;
        public const int personel_takip_ad_soyad = 2;
        public const int personel_takip_calistigi_yer = 3;
        public const int personel_takip_kart_id = 4;
        public const int personel_takip_tarih=5;
        public const int personel_takip_giris_cikis=6;
        public const int personel_takip_web_kayit=7;

        public const int personel_takip_eleman_Sayisi = 8;
        //
        //personel
        public const int personel_listesi_id = 0;
        public const int personel_listesi_web_id = 1;
        public const int personel_listesi_firma_web_id= 2;
        public const int personel_listesi_ad_soyad = 3;
        public const int personel_listesi_cinsiyet = 4;
        public const int personel_listesi_calistigi_yer = 5;
        public const int personel_listesi_ise_baslama_tar = 6;
        public const int personel_listesi_isten_ayrilma_tar = 7;
        public const int personel_listesi_tel = 8;
        public const int personel_listesi_adres = 9;
        public const int personel_listesi_kart_id = 10;
        public const int personel_listesi_is_durum = 11;
        public const int personel_listesi_web_kayit = 12;
        //
        //gecici_kart_takip
        public const int gecici_kart_takip_id = 0;
        public const int gecici_kart_takip_web_id = 1;
        public const int gecici_kart_takip_firma_web_id = 2;
        public const int gecici_kart_takip_kasiyer = 3;
        public const int gecici_kart_takip_personel = 4;
        public const int gecici_kart_takip_kart_id = 5;
        public const int gecici_kart_takip_kart_tip = 6;
        public const int gecici_kart_takip_yuklenen_miktar = 7;
        public const int gecici_kart_takip_kalan_miktar = 8;
        public const int gecici_kart_takip_kart_yer_bilgisi = 9;
        public const int gecici_kart_takip_tarih = 10;
        public const int gecici_kart_takip_web_kayit = 11;
        //










    }
}

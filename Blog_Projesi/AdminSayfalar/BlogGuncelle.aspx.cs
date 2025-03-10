﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog_Projesi.Entity;
namespace Blog_Projesi.AdminSayfalar
{
    public partial class BlogGuncelle : System.Web.UI.Page
    {
        BlogEntities db = new BlogEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            int y = int.Parse(Request.QueryString["BLOGID"]); //diğer sayfadan gelen BLOGID sonucunu y değişkenine ata

            if (Page.IsPostBack == false)
            {

                var turler = (from x in db.TBLTUR
                              select new
                              {
                                  x.TURAD,
                                  x.TURID
                              }).ToList();
                DropDownList1.DataSource = turler;
                DropDownList1.DataBind();

                var kategoriler = (from x in db.TBLKATEGORI
                                   select new
                                   {
                                       x.KATEGORIAD,
                                       x.KATEGORIID
                                   }).ToList();
                DropDownList2.DataSource = kategoriler;
                DropDownList2.DataBind();
                var deger = db.TBLBLOG.Find(y); // değişkenine göre TBLBLOG sonucunu bul
                TextBox1.Text = deger.BLOGBASLIK; // deger deb gelen BLOGBASLIK ı textbox a aktar
                TextBox2.Text = deger.BLOGTARIH.ToString();
                TextBox3.Text = deger.BLOKGORSEL;
                TextBox4.Text = deger.BLOGICERIK;
                DropDownList1.SelectedValue = deger.BLOGTUR.ToString(); //  Dropdown listten seçilen değer eşittir değerden gelen BLOGTURE eşittir.
                DropDownList2.SelectedValue = deger.BLOGKATEGORI.ToString();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int y = int.Parse(Request.QueryString["BLOGID"]);
            var blog = db.TBLBLOG.Find(y);
            blog.BLOGBASLIK = TextBox1.Text;
            blog.BLOGTARIH = DateTime.Parse(TextBox2.Text);
            blog.BLOKGORSEL = TextBox3.Text;
            blog.BLOGICERIK = TextBox4.Text;
            blog.BLOGTUR = byte.Parse(DropDownList1.SelectedValue);
            blog.BLOGKATEGORI = byte.Parse(DropDownList2.SelectedValue);
            db.SaveChanges();
            Response.Redirect("Bloglar.aspx");


        }
    }
}
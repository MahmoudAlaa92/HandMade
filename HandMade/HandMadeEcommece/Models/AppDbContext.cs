using System;
using System.Collections.Generic;
using HandMadeEcommece.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChildCategory> ChildCategories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<PaypalSetting> PaypalSettings { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImageGallery> ProductImageGalleries { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductReviewGallery> ProductReviewGalleries { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<ProductVariantItem> ProductVariantItems { get; set; }

    public virtual DbSet<PusherSetting> PusherSettings { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorCondition> VendorConditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7UN4QVRV; Database=ecommerce;Integrated Security = SSPI; TrustServerCertificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brands__3213E83FA9C9C1AF");

            entity.ToTable("brands");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Logo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F4D2DF785");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chats__3213E83F882259E0");

            entity.ToTable("chats");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Message)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.Seen)
                .HasDefaultValueSql("('0')")
                .HasColumnName("seen");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ChildCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__child_ca__3213E83F8446701B");

            entity.ToTable("child_categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_category_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.ChildCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_child_categories_categories");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coupons__3213E83F5AC3FD86");

            entity.ToTable("coupons");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("discount_type");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MaxUse).HasColumnName("max_use");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalUsed).HasColumnName("total_used");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F4A29B300");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Coupon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("coupon");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("currency_icon");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("currency_name");
            entity.Property(e => e.InvocieId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("invocie_id");
            entity.Property(e => e.OrderAddress)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("order_address");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ProductQty).HasColumnName("product_qty");
            entity.Property(e => e.ShppingMethod)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("shpping_method");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_users");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_pr__3213E83F23ED3B74");

            entity.ToTable("order_products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.UnitPrice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.VariantTotal)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("variant_total");
            entity.Property(e => e.Variants)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("variants");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_products_orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_order_products_products");

            entity.HasOne(d => d.Vendor).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_order_products_vendors");
        });

        modelBuilder.Entity<PaypalSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__paypal_s__3213E83F283819B4");

            entity.ToTable("paypal_settings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("currency_name");
            entity.Property(e => e.CurrencyRate).HasColumnName("currency_rate");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.SecretKey)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("secret_key");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FB1EBB987");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ChildCategoryId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("child_category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.IsApproved)
                .HasDefaultValueSql("('0')")
                .HasColumnName("is_approved");
            entity.Property(e => e.LongDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("long_description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OfferEndDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("offer_end_date");
            entity.Property(e => e.OfferPrice)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("offer_price");
            entity.Property(e => e.OfferStartDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("offer_start_date");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("product_type");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.SeoDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("seo_description");
            entity.Property(e => e.SeoTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("seo_title");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("short_description");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sku");
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubCategoryId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sub_category_id");
            entity.Property(e => e.ThumbImage)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("thumb_image");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.VideoLink)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("video_link");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_products_categories");

            entity.HasOne(d => d.ChildCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ChildCategoryId)
                .HasConstraintName("FK_products_child_categories");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_products_sub_categories");
        });

        modelBuilder.Entity<ProductImageGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F8A0DAFD3");

            entity.ToTable("product_image_galleries");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Image)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImageGalleries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_image_galleries_products");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FEF09A18C");

            entity.ToTable("product_reviews");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rating");
            entity.Property(e => e.Review)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("review");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_reviews_products");

            entity.HasOne(d => d.User).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_reviews_users");

            entity.HasOne(d => d.Vendor).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_reviews_vendors");
        });

        modelBuilder.Entity<ProductReviewGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F308ECF65");

            entity.ToTable("product_review_galleries");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Image)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.ProductReviewId).HasColumnName("product_review_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ProductReview).WithMany(p => p.ProductReviewGalleries)
                .HasForeignKey(d => d.ProductReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_review_galleries_product_reviews");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F11AFBFA9");

            entity.ToTable("product_variants");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_variants_products");
        });

        modelBuilder.Entity<ProductVariantItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FD52E1653");

            entity.ToTable("product_variant_items");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductVariantId).HasColumnName("product_variant_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductVariantItems)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_variant_items_product_variants");
        });

        modelBuilder.Entity<PusherSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pusher_s__3213E83F239F0E68");

            entity.ToTable("pusher_settings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.PusherAppId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pusher_app_id");
            entity.Property(e => e.PusherCluster)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pusher_cluster");
            entity.Property(e => e.PusherKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pusher_key");
            entity.Property(e => e.PusherSecret)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pusher_secret");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cate__3213E83F14571A2D");

            entity.ToTable("sub_categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sub_categories_categories");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83F4C013726");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.AmountRealCurrency).HasColumnName("amount_real_currency");
            entity.Property(e => e.AmountRealCurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("amount_real_currency_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_orders");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9966EF74");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.Image)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("phone");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("remember_token");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_add__3213E83F6297ACC6");

            entity.ToTable("user_addresses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zip");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_addresses_users");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vendors__3213E83F29DB38A1");

            entity.ToTable("vendors");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Banner)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("banner");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FbLink)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fb_link");
            entity.Property(e => e.InstaLink)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("insta_link");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ShopName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("shop_name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("('0')")
                .HasColumnName("status");
            entity.Property(e => e.TwLink)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tw_link");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vendors_users");
        });

        modelBuilder.Entity<VendorCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vendor_c__3213E83FCFD2B8A4");

            entity.ToTable("vendor_conditions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

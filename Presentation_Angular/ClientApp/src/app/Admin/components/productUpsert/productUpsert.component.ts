import { AlertifyService } from "./../../../Shared/services/alertify.service";
import { Subscription } from "rxjs";
import { Product } from "./../../../Shared/models/product";
import { ProductService } from "./../../../Shared/services/product.service";
import { Category } from "./../../../Shared/models/category.enum";
import { FormGroup, FormBuilder, Validators, AbstractControl } from "@angular/forms";
import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from "@angular/core";
import { Location } from "@angular/common";
import { PhotoService } from "../../services/photo.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-productUpsert",
  templateUrl: "./productUpsert.component.html",
  styleUrls: ["./productUpsert.component.css"]
})
export class ProductUpsertComponent implements OnInit, OnDestroy {
  productForm: FormGroup;
  productId: number;
  subscription: Subscription;

  selectedPhoto: File;
  defaultPhoto = "../../../../assets/default.jpg";
  photoUrl: any = this.defaultPhoto;
  @ViewChild("fileInput", null)
  fileInput: ElementRef;
  @ViewChild("fileLabel", null)
  fileLabel: ElementRef;

  category = Category;
  categoryKeys: number[];

  constructor(
    private productService: ProductService,
    private photoService: PhotoService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private location: Location
  ) {
  }

  ngOnInit() {
    this.createForm();

    this.productId = +this.route.snapshot.paramMap.get("id");

    if (this.productId !== 0) {
      this.subscription = this.route.data
        .subscribe((data: { product: Product }) => {
          this.updateForm(data.product["product"]);
        });
    }
    this.categoryKeys = Object.keys(this.category).filter(x => !isNaN(Number(x))).map(Number);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get form() { return this.productForm.controls; }

  create(p: Product): void {
    if (this.photoUrl !== this.productForm.get("photoUrl").value && this.photoUrl !== this.defaultPhoto) {
      this.uploadPhoto(p, "create");
    } else {
      this.createProduct(p);
    }
  }

  private updateForm(p: Product) {
    if (p.photoUrl !== null) {
      this.photoUrl = p.photoUrl;
    }

    this.productForm.setValue({
      id: p.id,
      name: p.name,
      price: p.price,
      category: p.category,
      description: p.description,
      photoUrl: p.photoUrl,
      photoId: p.photoId
    });
  }

  update(p: Product) {
    if (this.photoUrl !== this.productForm.get("photoUrl").value && this.photoUrl !== this.defaultPhoto) {
      this.uploadPhoto(p, "update");
    } else if (this.productForm.get("photoId").value !== null && this.photoUrl === this.defaultPhoto) {
      this.removePhoto(p);
    } else {
      this.updateProduct(p);
    }
  }

  cancel(): void {
    this.location.back();
  }

  onFileChanged(event): void {
    if (event.length === 0) {
      return;
    }

    // Enkel foto tonen & nog niet uploaden
    // Dit doen we bij het opslagen van het product
    this.selectedPhoto = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedPhoto);
    reader.onload = (_event) => this.photoUrl = reader.result;
  }

  onRemove(photoId: AbstractControl) {
    if (photoId) {
      this.photoUrl = this.defaultPhoto;
      this.productForm.patchValue({
        photoUrl: null
      });
    }
  }

  private createForm(): void {
    this.productForm = this.fb.group({
      id: [],
      name: ["", [Validators.required, Validators.maxLength(50)]],
      price: ["", [Validators.required, Validators.min(0)]],
      category: ["", Validators.required],
      description: [],
      photoUrl: [],
      photoId: []

    });
  }

  private uploadPhoto(p: Product, action: string): void {
    if (this.selectedPhoto) {
      const file = new FormData();
      file.append("photo", this.selectedPhoto, this.selectedPhoto.name);
      this.subscription = this.photoService.addPhoto(file).subscribe(x => {
        p.photoUrl = x.photoUrl;
        p.photoId = x.photoId;
        if (action === "create") {
          this.createProduct(p);
        } else if (action === "update") {
          this.updateProduct(p);
        }
      });
    }
  }

  private removePhoto(p: Product) {
    this.subscription = this.photoService.removePhoto(p.photoId)
      .subscribe(() => {
          const product = this.productForm.value;
          product.fotoUrl = null;
          product.fotoId = null;
          this.subscription = this.productService.update(this.productId, p)
            .subscribe(() => {
              this.alertify.success("Photo deleted");
              this.location.back();
            });
        },
        () => this.alertify.error("Error at deleting photo"));
  }

  private createProduct(p: Product): void {
    this.subscription = this.productService.create(p)
      .subscribe(() => this.location.back(),
        error => this.alertify.error(error));
  }

  private updateProduct(p: Product): void {
    this.subscription = this.productService.update(this.productId, p)
      .subscribe(() => this.location.back(),
        error => this.alertify.error(error));
  }
}

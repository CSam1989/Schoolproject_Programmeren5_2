import { Product } from "./../models/product";
import { Injectable } from "@angular/core";
import { ShoppingCartItem } from "../models/shoppingCartItem";

@Injectable({
  providedIn: "root"
})
export class ShoppingCartService {
  shoppingCart: ShoppingCartItem[];
  private VAT = 21;

  constructor() {
    this.getLocalStorage();
  }

  getItem(product: Product): ShoppingCartItem {
    return this.shoppingCart.find(item => item.product.id === product.id);
  }

  getItemCount(product: Product): number {
    const item = this.getItem(product);
    if (item) {
      return item.quantity;
    }
    return 0;
  }

  getItemTotalPrice(product: Product): number {
    const item = this.getItem(product);

    return (item.quantity * item.product.price);
  }

  get totalCount(): number {
    let count = 0;

    this.shoppingCart.forEach(item => {
      count += item.quantity;
    });

    return count;
  }

  get totalPrice(): number {
    let price = 0;

    this.shoppingCart.forEach(item => {
      price += (item.quantity * item.product.price);
    });

    return price;
  }

  increment(product: Product): void {
    const cartItem = this.getItem(product);

    if (cartItem) {
      const index = this.shoppingCart.indexOf(cartItem);
      this.shoppingCart[index].quantity++;
    } else {
      this.addToCart(product);
    }

    this.setLocalStorage();
  }

  decrement(product: Product): void {
    const cartItem = this.getItem(product);

    if (cartItem && cartItem.quantity >= 1) {
      const index = this.shoppingCart.indexOf(cartItem);
      this.shoppingCart[index].quantity--;

      if (this.shoppingCart[index].quantity === 0) {
        this.shoppingCart.splice(index, 1);
      }
    }
    this.setLocalStorage();
  }

  addToCart(product: Product) {
    const cartItem = this.getItem(product);

    if (!cartItem) {
      this.shoppingCart.push({
        product: product,
        quantity: 1
      });
    }
  }

  removeFromCart(product: Product): void {
    const cartItem = this.getItem(product);

    if (cartItem) {
      const index = this.shoppingCart.indexOf(cartItem);
      this.shoppingCart.splice(index, 1);
      this.setLocalStorage();
    }
  }

  clearAll() {
    this.shoppingCart = [];
    localStorage.removeItem("shoppingCart");
  }

  private getLocalStorage(): void {
    this.shoppingCart = JSON.parse(localStorage.getItem("shoppingCart"));

    if (!this.shoppingCart) {
      this.shoppingCart = [];
    }
  }

  private setLocalStorage(): void {
    localStorage.setItem("shoppingCart", JSON.stringify(this.shoppingCart));
  }
}

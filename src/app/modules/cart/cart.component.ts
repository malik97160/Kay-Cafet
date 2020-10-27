import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { Product } from 'src/app/Interfaces/product';
import { CartItems } from './mock-cart-items';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  hasCartItems: boolean; 
  cartItems: any;
  totalPrice: number;
  constructor(public dialogRef: MatDialogRef<CartComponent>) { }

  ngOnInit(): void {
    this.cartItems = CartItems
    this.setHasCartItem();
    this.calculTotalPrice();
  }

  private setHasCartItem() {
    this.hasCartItems = this.cartItems.length > 0;
  }

  closeDialog(){
    this.dialogRef.close();
  }

  incrementItemCounter(item: Product){
    item.Quantity +=1;
    this.calculTotalPrice();
  }

  decrementItemCounter(item: Product){
    item.Quantity-=1;
    if(item.Quantity <= 0){
      item.Quantity = 0;
      this.cartItems = this.cartItems.filter(cartItem => cartItem != item);
      this.setHasCartItem();
    }
    this.calculTotalPrice(); 
  }

  private calculTotalPrice(){
    this.totalPrice = 0;
    this.cartItems.forEach(item => {
        this.totalPrice += item.UnitPrice * item.Quantity;
    });
  }
}

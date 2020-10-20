import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { CartItems } from './mock-cart-items';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  hasCartItems: boolean; 
  cartItems: any;
  constructor(public dialogRef: MatDialogRef<CartComponent>) { }

  ngOnInit(): void {
    this.cartItems = [];//CartItems
    this.hasCartItems = this.cartItems.length > 0;
  }

  closeDialog(){
    this.dialogRef.close();
  }

  incrementItemCounter(){}

  decrementItemCounter(){}
}

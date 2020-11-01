import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { CartService } from 'src/app/core/services/cart/cart.service';
import { Product } from 'src/app/Interfaces/product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  hasCartItems: boolean; 
  cartItems: any;
  totalPrice: number;
  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: CartService, private router: Router) { }

  ngOnInit(): void {
    this.cartItems = this.cartService.getProductsFromBasket();
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
    this.cartService.updateProductInBasket(item);
    this.calculTotalPrice();
  }

  decrementItemCounter(item: Product){
    item.Quantity-=1;
    if(item.Quantity <= 0){
      item.Quantity = 0;
      this.cartService.removeProductInBasket(item);
      this.cartItems = this.cartItems.filter(cartItem => cartItem != item);
      this.setHasCartItem();
    }
    else{
      this.cartService.updateProductInBasket(item);
    }
    this.calculTotalPrice(); 
  }

  confirmOrder(){
    this.router.navigate(['validation']);
    this.closeDialog();
  }

  private calculTotalPrice(){
    this.totalPrice = 0;
    this.cartItems.forEach(item => {
        this.totalPrice += item.UnitPrice * item.Quantity;
    });
  }
}

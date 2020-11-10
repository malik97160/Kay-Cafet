import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartService } from 'src/app/core/services/cart/cart.service';
import { Product } from 'src/app/Interfaces/product';

@Component({
  selector: 'app-cart-rows',
  templateUrl: './cart-rows.component.html',
  styleUrls: ['./cart-rows.component.scss']
})
export class CartRowsComponent implements OnInit {
  hasCartItems: boolean; 
  cartItems: any;
  totalPrice: number;
  @Input() isFromBasket: boolean;
  @Output() totalPriceEvent = new EventEmitter<number>();
  @Output() hasCartItemEvent = new EventEmitter<boolean>();
  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartItems = this.cartService.getProductsFromBasket();
    this.setHasCartItem();
    this.calculTotalPrice();
  }

  private setHasCartItem() {
    this.hasCartItems = this.cartItems.length > 0;
    this.hasCartItemEvent.emit(this.hasCartItems);
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

  private calculTotalPrice(){
    this.totalPrice = 0;
    this.cartItems.forEach(item => {
        this.totalPrice += item.UnitPrice * item.Quantity;
    });
    this.totalPriceEvent.emit(this.totalPrice);
  }
}

import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/core/services/cart/cart.service';
import { ConfirmDialogService } from 'src/app/core/services/dialog/confirm-dialog.service';
import { Product } from 'src/app/Interfaces/product';
import { CartComponent } from 'src/app/modules/cart/cart.component';

@Component({
  selector: 'app-cart-status',
  templateUrl: './cart-status.component.html',
  styleUrls: ['./cart-status.component.scss']
})
export class CartStatusComponent implements OnInit {
  basketItemAddedSubscription: any;
  basketItemCount: number = 0;
  basketDroppedSubscription: any;

  constructor(private cartService: CartService, private dialogService: ConfirmDialogService) { }

  ngOnInit(): void {
    let basket = this.cartService.getProductsFromBasket();
    this.basketItemCount = this.computeBasketCount(basket);
      this.basketItemAddedSubscription = this.cartService.basketChanged$.subscribe(
      item => {
            let basket = this.cartService.getProductsFromBasket();
            if (basket)
              this.basketItemCount = this.computeBasketCount(basket);
            });

  // Subscribe to Drop Basket Observable: 
  this.basketDroppedSubscription = this.cartService.basketDroped$.subscribe(res => {
      this.basketItemCount = 0;
  });
  }

  private computeBasketCount(basket: Product[]): number {
    return basket.reduce((sum, current) => sum + current.Quantity, 0);
  }

  displayCartDialog(){
    this.dialogService.openDialog(CartComponent, '28rem', '100%', 'chart-dialog', null, 0);
  }

}

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart-status',
  templateUrl: './cart-status.component.html',
  styleUrls: ['./cart-status.component.scss']
})
export class CartStatusComponent implements OnInit {
  basketItemAddedSubscription: any;
  basketProductsCount: number = 0;

  constructor() { }

  ngOnInit(): void {
    /*this.basketItemAddedSubscription = this.basketEvents.addItemToBasket$.subscribe(
      item => {
          this.service.addItemToBasket(item).subscribe(res => {
              this.service.getBasket().subscribe(basket => {
                  if (basket)
                      this.basketProductsCount = basket.items.length;
              });
          });
      });

  // Subscribe to Drop Basket Observable: 
  this.basketDroppedSubscription = this.service.basketDroped$.subscribe(res => {
      this.badge = 0;
  });*/
  }

}

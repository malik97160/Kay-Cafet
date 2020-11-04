import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/core/services/cart/cart.service';

@Component({
  selector: 'app-validation',
  templateUrl: './validation.component.html',
  styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {
  totalAmount: number;

  constructor(private _router: Router, private _cartService: CartService) { }

  ngOnInit(): void {
  }

  goBackToHomePage(){
    this._router.navigate(["/"]);
  }

  setTotalPrice($event){
    this.totalAmount = $event;
  }
}

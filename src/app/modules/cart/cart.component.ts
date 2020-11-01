import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  totalPrice: number;
  hasCartItems: boolean;
  constructor(public dialogRef: MatDialogRef<CartComponent>, private router: Router) { }

  ngOnInit(): void {
  }

  closeDialog(){
    this.dialogRef.close();
  }

  confirmOrder(){
    this.router.navigate(['validation']);
    this.closeDialog();
  }

  setTotalPrice($event){
    this.totalPrice = $event
  }

  setHasCartItems($event){
    this.hasCartItems = $event;
  }
}

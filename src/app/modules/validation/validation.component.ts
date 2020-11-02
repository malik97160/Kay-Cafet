import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-validation',
  templateUrl: './validation.component.html',
  styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {
  totalAmount: number;

  constructor(private _router: Router) { }

  ngOnInit(): void {
    this.totalAmount = 0;
  }

  goBackToHomePage(){
    this._router.navigate(["/"]);
  }
}

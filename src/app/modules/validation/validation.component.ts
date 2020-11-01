import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-validation',
  templateUrl: './validation.component.html',
  styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {

  constructor(private _router: Router) { }

  ngOnInit(): void {
  }

  goBackToHomePage(){
    this._router.navigate(["/"]);
  }
}

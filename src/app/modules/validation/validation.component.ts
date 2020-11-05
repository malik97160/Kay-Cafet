import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { User } from 'src/app/Interfaces/user';

@Component({
  selector: 'app-validation',
  templateUrl: './validation.component.html',
  styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {
  totalAmount: number;
  validOrderForm: FormGroup;
  user: User;
  constructor(private _router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    const phoneregex = "^(\\d(\\s)?){10}$";
    this.user = {
      Email: "malik.couchy@gmail.com",
      FirstName: "malik",
      LastName: "couchy",
      PhoneNumber: "0678963541"
    };
    this.validOrderForm = this.fb.group({
      fullName: '', 
      phoneNumber: [this.user.PhoneNumber, [Validators.pattern(phoneregex)]],
      email: [this.user.Email, [
        Validators.required,
        Validators.email
      ]],
      agree: ''
    })
  }

  goBackToHomePage(){
    this._router.navigate(["/"]);
  }

  setTotalPrice($event){
    this.totalAmount = $event;
  }

  get email(){
    return this.validOrderForm.get('email');
  }

  get fullName(){
    return this.validOrderForm.get('fullName');
  }

  get phoneNumber(){
    return this.validOrderForm.get('phoneNumber');
  }
}

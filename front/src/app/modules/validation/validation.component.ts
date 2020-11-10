import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { User } from 'src/app/Interfaces/user';
import { PhoneNumberValidator } from 'src/app/core/validators/phone-number.validator';

@Component({
  selector: 'app-validation',
  templateUrl: './validation.component.html',
  styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {
  totalAmount: number;
  validOrderForm: FormGroup;
  user: User;
  submitted: boolean = false;
  constructor(private fb: FormBuilder, private _router: Router) { }

  ngOnInit(): void {
    this.user = {
      Email: "malik.couchy@gmail.com",
      FirstName: "malik",
      LastName: "couchy",
      PhoneNumber: "0678963541"
    };
    this.validOrderForm = this.fb.group({
      phoneNumber: [this.user.PhoneNumber, [PhoneNumberValidator()]],
      email: [this.user.Email, [
        Validators.required,
        Validators.email
      ]],
      agree: [false, Validators.requiredTrue],
      comment: ''
    })
  }

  onSubmit(){
    this.submitted = true;

    if(this.validOrderForm.invalid){
      return;
    }

    alert(JSON.stringify(this.validOrderForm.value));
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

  get agree(){
    return this.validOrderForm.get('agree');
  }
}

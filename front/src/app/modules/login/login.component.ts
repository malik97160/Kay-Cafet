import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { LoginPayload } from 'src/app/core/services/auth/login-paylaod';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  signUpButton: HTMLElement;
  signInButton: HTMLElement;
  container: HTMLElement;
  signInForm: FormGroup;
  signInFormSubmitted: boolean = false;
  constructor(private authService: AuthService, private formBuilder: FormBuilder) {
   }

  ngOnInit(): void {
    this.signInForm = this.formBuilder.group({
      login: ['', [Validators.required, Validators.maxLength(20)]],
      password: ['', [Validators.required]]
    })
  }

  addRightPanelActive(){
    document.getElementById('container').classList.add('right-panel-active');
  }

  removeRightPanelActive(){
    document.getElementById('container').classList.remove('right-panel-active');
  }

  async signIn(){
    debugger;
    this.signInFormSubmitted = true;
    
    if (this.signInForm.invalid)
      return false;

      var payload: LoginPayload = this.signInForm.value;
      await this.authService.signIn(payload);
  }

  get login(){
    return this.signInForm.get('login');
  }

  get password(){
    return this.signInForm.get('password');
  }
}

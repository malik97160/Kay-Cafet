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

  signIn(){
    debugger;
    if (this.signInForm.invalid)
      return false;

    var payload: LoginPayload = this.signInForm.value;
    this.authService.login(payload);
  }
}

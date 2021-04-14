import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { LoginPayload } from 'src/app/core/services/auth/login-paylaod';
import { single, take, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { LocalStorateJwtService } from 'src/app/core/services/auth/local-storate-jwt.service';

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
  private readonly destroy$ = new Subject();
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private jwtService: LocalStorateJwtService) {
   }

  ngOnInit(): void {
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.maxLength(20)]],
      password: ['', [Validators.required]]
    })
  }

  ngOnDestroy(){
    this.destroy$.next();
    this.destroy$.complete();
  }

  addRightPanelActive(){
    document.getElementById('container').classList.add('right-panel-active');
  }

  removeRightPanelActive(){
    document.getElementById('container').classList.remove('right-panel-active');
  }

  public signIn(){
    this.signInFormSubmitted = true;
    
    if (this.signInForm.invalid)
      return false;

      var payload: LoginPayload = this.signInForm.value;
      this.authService.login(payload).pipe(
        single()
      ).subscribe(tokens => {
        if(tokens){
          debugger;
          this.jwtService.setToken(tokens.jwtToken);
          this.jwtService.setToken(tokens.refreshToken);
        }
      });
  }

  get username(){
    return this.signInForm.get('username');
  }

  get password(){
    return this.signInForm.get('password');
  }
}

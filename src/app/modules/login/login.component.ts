import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  signUpButton: HTMLElement;
  signInButton: HTMLElement;
  container: HTMLElement;
  constructor() {
    // this.signUpButton = document.getElementById('signUp');
    // this.signInButton = document.getElementById('signIn');
    // this.container = document.getElementById('container');
   }

  ngOnInit(): void {
  }

  addRightPanelActive(){
    document.getElementById('container').classList.add('right-panel-active');
  }

  removeRightPanelActive(){
    document.getElementById('container').classList.remove('right-panel-active');

  }
}

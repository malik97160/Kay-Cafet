import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ConfirmDialogService } from 'src/app/core/services/dialog/confirm-dialog.service';
import { Router }                       from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
@Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();
  isAdminPath: boolean;
  isHomePage: boolean;

  constructor(public dialogService: ConfirmDialogService, private _router: Router) {
    this.isAdminPath = this._router.url.includes('/admin');
    this.isHomePage = !this._router.url.includes('/admin')  //this._router.url === "/";
   }

  ngOnInit() {
  }

  toggleSideBar(){
    this.toggleSideBarForMe.emit();
  }

  goBackToHomePage(){
    this._router.navigate(["/"]);
  }
}

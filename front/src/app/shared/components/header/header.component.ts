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
  urlChangedSubsription: any;

  constructor(public dialogService: ConfirmDialogService, private _router: Router) {
    this.isAdminPath = this._router.url.includes('/admin');
  }
  
  ngOnInit() {
    this.checkIfIsHomePage();
    this.urlChangedSubsription = this._router.events.subscribe((routerHasChanged) => { 
      if(routerHasChanged){
        this.checkIfIsHomePage();
      }
    })
  }
  
  toggleSideBar(){
    this.toggleSideBarForMe.emit();
  }
  
  goBackToHomePage(){
    this._router.navigate(["/"]);
    this.checkIfIsHomePage();
  }
  
  checkIfIsHomePage(){
    this.isHomePage = this._router.url === "/";
  }

  ngOnDestroy(){
    if (this.urlChangedSubsription)
      this.urlChangedSubsription.unsubscribe();
  }
}

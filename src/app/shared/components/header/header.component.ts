import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ConfirmDialogService } from 'src/app/core/services/dialog/confirm-dialog.service';
import { CartComponent } from 'src/app/modules/cart/cart.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
@Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();

  constructor(public dialogService: ConfirmDialogService) { }

  ngOnInit() {
  }

  toggleSideBar(){
    this.toggleSideBarForMe.emit();
  }

  displayCartDialog(){
    this.dialogService.openDialog(CartComponent, '28rem', '100%', 'chart-dialog');
  }
}

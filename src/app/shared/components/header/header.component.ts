import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ConfirmDialogService } from 'src/app/core/services/dialog/confirm-dialog.service';

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
    this.dialogService.openDialog("chart dialog", '30rem', '100%', 'chart-dialog');
  }
}

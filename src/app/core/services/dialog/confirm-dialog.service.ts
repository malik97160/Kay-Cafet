import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  constructor(private dialog: MatDialog) { }

  openConfirmDialog(message:string){
    return this.dialog.open(ConfirmDialogComponent, {
      width: '450px',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      data : {
        message: message
      }
    });

  }
}

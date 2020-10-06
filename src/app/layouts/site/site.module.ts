import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { SiteComponent } from './site.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    SiteComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule
  ]
})
export class SiteModule { }

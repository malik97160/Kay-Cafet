import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatDividerModule, MatSidenavModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { from } from 'rxjs';
import { DashboadComponent } from 'src/app/modules/dashboad/dashboad.component';
import { PostsComponent } from 'src/app/modules/posts/posts.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AdminComponent } from './admin.component';
import {MatTableModule } from '@angular/material';

@NgModule({
  declarations: [
    AdminComponent,
    DashboadComponent,
    PostsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    MatSidenavModule,
    MatDividerModule,
    FlexLayoutModule,
    MatTableModule
  ]
})
export class AdminModule { }

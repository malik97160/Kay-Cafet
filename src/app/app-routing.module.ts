import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './layouts/admin/admin.component';
import { DashboadComponent } from './modules/dashboad/dashboad.component';
import { PostsComponent } from './modules/posts/posts.component';

const routes: Routes = [{
  path:'',
  component: AdminComponent,
  children: [{
    path: '', 
    component: DashboadComponent
  }, {
    path: 'posts',
    component: PostsComponent
  }
]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './layouts/admin/admin.component';
import { SiteComponent } from './layouts/site/site.component';
import { DashboadComponent } from './modules/dashboad/dashboad.component';
import { HomeComponent } from './modules/home/home.component';
import { PostsComponent } from './modules/posts/posts.component';

const routes: Routes = [
  //Site routing
  {
    path: '',
    component: SiteComponent,
    children: [
      {path: '', component: HomeComponent, pathMatch: 'full'}
    ]
  },

  //Admin routing
  {
  path:'admin',
  component: AdminComponent,
  children: [
    {path: '', component: DashboadComponent}, 
    {path: 'posts', component: PostsComponent}
]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

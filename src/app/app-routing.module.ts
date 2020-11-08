import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { AdminComponent } from './layouts/admin/admin.component';
import { SiteComponent } from './layouts/site/site.component';
import { DashboadComponent } from './modules/dashboad/dashboad.component';
import { HomeComponent } from './modules/home/home.component';
import { LoginComponent } from './modules/login/login.component';
import { PostsComponent } from './modules/posts/posts.component';
import { ValidationComponent } from './modules/validation/validation.component';

const routes: Routes = [
  //Site routing
  {
    path: '',
    component: SiteComponent,
    children: [
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'validation', component: ValidationComponent, canActivate: [AuthGuard]}
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
},

{
  path: 'login',
  component: LoginComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

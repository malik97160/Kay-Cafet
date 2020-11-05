import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminModule } from './layouts/admin/admin.module';
import { MatFormFieldModule, MatIconModule, MatInputModule, MatSelectModule } from '@angular/material';
import { HomeComponent } from './modules/home/home.component';
import { SiteModule } from './layouts/site/site.module';
import { CartComponent } from './modules/cart/cart.component';
import { LoginComponent } from './modules/login/login.component';
import { ValidationComponent } from './modules/validation/validation.component';
import { registerLocaleData } from '@angular/common';
import localeFR from '@angular/common/locales/fr';
import { CartRowsComponent } from './modules/cart/cart-rows/cart-rows.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

registerLocaleData(localeFR);
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CartComponent,
    LoginComponent,
    ValidationComponent,
    CartRowsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AdminModule,
    SiteModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule 
  ],
  providers: [{
    provide: LOCALE_ID,
    useValue: 'fr'
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

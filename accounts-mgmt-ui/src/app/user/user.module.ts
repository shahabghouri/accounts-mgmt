import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { BalanceComponent } from './balance/balance.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularDeviceInformationService } from 'angular-device-information';

import { HttpClientModule } from '@angular/common/http';
import { UserService } from '../services/user.service';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { IpService } from '../services/ip.service';

@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    BalanceComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    FormsModule
  ],
  providers:[UserService, IpService, AngularDeviceInformationService]
})
export class UserModule { }

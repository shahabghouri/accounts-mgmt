import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { IpService } from '../../services/ip.service';
import { AngularDeviceInformationService } from 'angular-device-information';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = "";
  ipAddress: string = "";
  constructor(private ipService:IpService,private deviceInformationService: AngularDeviceInformationService,private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      // ipaddress: ['', Validators.required],
      // device: ['', Validators.required],
      // browser: ['', Validators.required]
    });
  }
  ngOnInit(){
    this.getIP()
  }
  getIP()  
  {  
    this.ipService.getIPAddress().subscribe((res:any)=>{  
      this.ipAddress=res.ip;  
    });  
  }  
  onSubmit() {
    if (this.loginForm.valid) {
      var loginObj =  {
        Username: this.loginForm.value.username,
        Password: this.loginForm.value.password,
        IPAddress: this.ipAddress,
        Browser: this.deviceInformationService.getDeviceInfo().browser,
        Device: this.deviceInformationService.getDeviceInfo().userAgent
      }
      this.userService.authenticate(loginObj).subscribe(response => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['/user/balance']);
      }, errorResponse => {
        this.errorMessage = errorResponse.error.message;
      });
    }
  }
}
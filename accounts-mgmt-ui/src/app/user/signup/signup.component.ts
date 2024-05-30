import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { AngularDeviceInformationService } from 'angular-device-information';
import { IpService } from '../../services/ip.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit{
  signupForm: FormGroup;
  errorMessage: string = "";
  ipAddress: string = "";
  constructor(private ipService:IpService,private deviceInformationService: AngularDeviceInformationService,private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.signupForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
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
    if (this.signupForm.valid) {
      var signUpObj =  {
        FirstName: this.signupForm.value.firstname,
        LastName: this.signupForm.value.lastname,
        Password: this.signupForm.value.password,
        Username: this.signupForm.value.username,
        IPAddress: this.ipAddress,
        Device: this.deviceInformationService.getDeviceInfo().userAgent
      }
      this.userService.signup(signUpObj).subscribe(response => {
        this.errorMessage = "";
        this.router.navigate(['/user/login']);
      }, errorResponse => {
        this.errorMessage = errorResponse.error.Errors.join('<br/>');
      });
    }
  }
}
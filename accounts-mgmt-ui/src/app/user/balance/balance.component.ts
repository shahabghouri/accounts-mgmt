import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-balance',
  templateUrl: './balance.component.html',
  styleUrls: ['./balance.component.css']
})
export class BalanceComponent implements OnInit {
  balance: number = 0;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.userService.getBalance(token).subscribe(response => {
        this.balance = response.balance;
      }, error => {
        console.error('Failed to fetch balance', error);
      });
    }
  }
  Logout(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any[] = [];
  private accountService = inject(AccountService);
  private readonly apiAddress: string = 'https://localhost:5001/api/users';
  private readonly successMessage: string = 'Request was successful';

  // to inject you can also use httpe = inject(HttpClient)
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  getUsers(): void {
    this.http.get(this.apiAddress).subscribe({
      next: (response) => (this.users = response as any[]),
      error: (error) => console.log(error),
      complete: () => console.log(this.successMessage),
    });
  }
}

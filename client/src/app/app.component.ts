import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: User[] = [];
  private readonly apiAddress: string = 'https://localhost:5001/api/users';
  private readonly successMessage: string = 'Request was successful';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get(this.apiAddress).subscribe({
      next: (response) => (this.users = response as User[]),
      error: (error) => console.log(error),
      complete: () => console.log(this.successMessage),
    });
  }
}

interface User {
  id: number;
  userName: string;
}

import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  registerMode = false;
  private readonly apiAddress: string = 'https://localhost:5001/api/users';
  private readonly successMessage: string = 'Request was successful';
  users: any[] = [];

  constructor(private http: HttpClient) {}

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers(): void {
    this.http.get(this.apiAddress).subscribe({
      next: (response) => (this.users = response as any[]),
      error: (error) => console.log(error),
      complete: () => console.log(this.successMessage),
    });
  }
  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }
}

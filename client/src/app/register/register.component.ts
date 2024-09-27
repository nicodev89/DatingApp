import { Component } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: any = {};

  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('cancelled');
  }
}

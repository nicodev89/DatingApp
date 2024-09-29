import { Component, EventEmitter, inject, input, Output, output } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: any = {};
  private accountService = inject(AccountService);
 cancelRegister = output<boolean>();

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: err => console.log(err)
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

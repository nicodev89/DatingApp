import {
  Component,
  HostListener,
  inject,
  OnInit,
  ViewChild,
  viewChild,
} from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css',
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  ngOnInit(): void {
    this.loadMember();
  }
  member?: Member;
  private memberService = inject(MembersService);
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) {
      return;
    }

    this.memberService.getMember(user.username).subscribe({
      next: (member) => (this.member = member),
    });
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: (_) => {
        this.toastr.success('Profile Updated successfully!');
        this.editForm?.reset(this.member);
      },
    });
  }
}

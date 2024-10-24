import { Component, inject, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { MembersCardsComponent } from "../members-cards/members-cards.component";

@Component({
    selector: 'app-members-list',
    standalone: true,
    templateUrl: './members-list.component.html',
    styleUrl: './members-list.component.css',
    imports: [MembersCardsComponent]
})
export class MembersListComponent implements OnInit {
  memberService = inject(MembersService);

  ngOnInit(): void {
    if(this.memberService.members().length === 0) this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers();
  }
}

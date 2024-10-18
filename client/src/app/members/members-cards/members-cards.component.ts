import { Component, inject, input, ViewEncapsulation } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-members-cards',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './members-cards.component.html',
  styleUrl: './members-cards.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class MembersCardsComponent {
  member = input.required<Member>();
}

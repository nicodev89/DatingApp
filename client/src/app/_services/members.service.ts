import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
      next: (members) => this.members.set(members),
    });
  }

  getMember(username: string) {
    const member = this.members().find((x) => x.userName === username);
    // we need to return an observable for our component (so we use of() method)
    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  // the member we are passing is simply the values inserted in the form to modify profile
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      // tap() allows to create side effects (basically touch observable without changing it)
      tap(() =>
        // loop through memebrs and find the one we updatedand change that. We will assign the new value when we find it
        // otherwise we reassign the old value
        this.members.update((members) =>
          members.map((m) => (m.userName === member.userName ? member : m))
        )
      )
    );
  }
}

import { importProvidersFrom, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
  BrowserAnimationsModule,
  provideAnimations,
} from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { provideRouter } from '@angular/router';
import { provideToastr } from 'ngx-toastr';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { loadingInterceptor } from './_interceptors/loading.interceptor';

@NgModule({
  declarations: [AppComponent, HomeComponent],
  bootstrap: [AppComponent],
  providers: [
    provideHttpClient(
      withInterceptors([errorInterceptor, jwtInterceptor, loadingInterceptor])
    ),
    provideAnimations(),
    provideToastr({
      positionClass: 'toast-bottom-right',
    }),
    importProvidersFrom(NgxSpinnerModule),
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    RegisterComponent,
    NavComponent,
    NgxSpinnerModule,
  ],
})
export class AppModule {}

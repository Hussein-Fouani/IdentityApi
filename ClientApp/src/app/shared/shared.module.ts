import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotfoundComponent } from './components/errors/notfound/notfound.component';
import { ValidationMessagesComponent } from './components/errors/validation-messages/validation-messages.component';



@NgModule({
  declarations: [
    NotfoundComponent,
    ValidationMessagesComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }

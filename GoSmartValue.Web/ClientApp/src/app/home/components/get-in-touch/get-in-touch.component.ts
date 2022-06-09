import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ContactService} from "../../../shared/services/contact.service";
import {Login} from "../../../auth/models/login";
import {Contact} from "../../../shared/models/contact";
import {HttpErrorResponse} from "@angular/common/http";
import Swal from "sweetalert2";

@Component({
  selector: 'app-get-in-touch',
  templateUrl: './get-in-touch.component.html',
  styleUrls: ['./get-in-touch.component.scss']
})
export class GetInTouchComponent implements OnInit {


  contactForm: FormGroup = new FormGroup({});
  contactData: Contact = new Contact();
  submitted: boolean = false;

  constructor(private fb: FormBuilder,
              private contactService: ContactService) { }

  get f(): { [key: string]: AbstractControl } { return this.contactForm.controls; }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(){
    this.contactForm = this.fb.group({
      firstname: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      email: ['', [Validators.required]],
      subject: ['', [Validators.required]],
      message: ['', [Validators.required]]
    })
  }

  onSubmit(){


    this.contactData = <Contact>{
      firstname: this.f.firstname.value,
      lastname: this.f.lastname.value,
      phone: this.f.phone.value,
      email: this.f.email.value,
      subject: this.f.subject.value,
      message: this.f.message.value
    };

    this.sendMessage();
  }

  sendMessage(){
  }

}

import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  message: string;
  title: string;
}


@Component({
  selector: 'app-dialog-message',
  templateUrl: './dialog-message.component.html',
  styleUrls: ['./dialog-message.component.sass']
})
export class DialogMessageComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) {}
}

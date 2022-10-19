import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertModel } from 'src/app/models/alert.model';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'alert', templateUrl: 'alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  message: AlertModel;
  hidden: boolean = true;
  constructor(private alertService: AlertService) {
  }

  ngOnInit() {
    this.subscription = this.alertService.getAlert()
      .subscribe(message => {
        switch (message && message.type) {
          case 'success':
            message.cssClass = 'alert alert-success';
            this.hidden = false;
            break;
          case 'error':
            message.cssClass = 'alert alert-danger';
            this.hidden = false;
            break;
        }

        this.message = message;
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  close() {
    this.hidden = true;
  }
}
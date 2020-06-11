import { OrderService } from "./../services/order.service";
import { Injectable, OnDestroy } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from "@angular/router";
import { Observable, Subscription } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class OrderGuard implements CanActivate, OnDestroy {
  subscription: Subscription;

  constructor(
    private orderService: OrderService,
    private router: Router,
  ) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // Backend controleert of de gevraagde order wel tot dit account hoort
    // Andere klant => error
    // Admin kan alles opvragen
    this.subscription = this.orderService.getById(+next.paramMap.get("id"))
      .subscribe((data) => {
          return true;
        },
        (error) => {
          if (error === "Forbidden Access") {
            this.router.navigate(["/access-denied"]);
          } else {
            this.router.navigate(["**"]);
          }
          return false;
        });
    return true;
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}

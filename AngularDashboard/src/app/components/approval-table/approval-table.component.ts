import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Approval } from '../../core/approval/approval.types';
import { ApprovalService } from '../../core/approval/approval.service';
import { ApprovalStatusTypes } from '../../core/approval/approval-status.types';

@Component({
    selector: 'approval-table',
    standalone: true,
    templateUrl: './approval-table.component.html',
    encapsulation: ViewEncapsulation.None,
    imports: [CommonModule]
})
export class ApprovalTableComponent implements OnInit {
    approvals: Approval[] = [];

    constructor(private approvalService: ApprovalService) {}

    private getApprovals(): void {
        this.approvalService.getApprovals().subscribe(data => {
            data.forEach(approval => {
                approval.Status = ApprovalStatusTypes[parseInt(approval.Status, 10)];
            });
            this.approvals = data;
        });
    }

    ngOnInit(): void {
        this.getApprovals();
    }

    refreshApprovals(): void {
        this.getApprovals();
    }

    createApproval(): void {
        this.approvalService.createApproval().subscribe({
            next: () => {
                // Refresh the approvals list after creating a new approval
                this.getApprovals();
            },
            error: (err) => {
                // Handle error (optional: show a message)
                console.error('Failed to create approval', err);
            }
        });
    }

    startApproval(requestId: string): void {
        this.approvalService.startApproval(requestId).subscribe({
            next: () => {
                // Refresh the approvals list after starting an approval
                this.getApprovals();
            },
            error: (err) => {
                // Handle error (optional: show a message)
                console.error('Failed to start approval', err);
            }
        });
    }

    approve(instanceId: string): void {
        this.approvalService.approve(instanceId).subscribe({
            next: () => {
                // Refresh the approvals list after approving
                setTimeout(() => {
                    this.getApprovals();
                }, 2000);
            },
            error: (err) => {
                // Handle error (optional: show a message)
                console.error('Failed to approve', err);
            }
        });
    }

    reject(instanceId: string): void {
        this.approvalService.reject(instanceId).subscribe({
            next: () => {
                // Refresh the approvals list after rejecting
                setTimeout(() => {
                    this.getApprovals();
                }, 2000);
            },
            error: (err) => {
                // Handle error (optional: show a message)
                console.error('Failed to reject', err);
            }
        });
    }
}